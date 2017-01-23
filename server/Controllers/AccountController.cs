using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Server.Controllers.Filters;
using Server.Models;
using Server.Models.AccountViewModels;
using Server.Services;

namespace Server.Controllers
{
  [Authorize]
  [SecurityHeaders]
  public class AccountController : Controller
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger _logger;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly AccountService _account;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILoggerFactory loggerFactory,
        IIdentityServerInteractionService interaction,
        IHttpContextAccessor httpContext,
        IClientStore clientStore)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = loggerFactory.CreateLogger<AccountController>();
      _interaction = interaction;
      _clientStore = clientStore;
      _account = new AccountService(interaction, httpContext, clientStore);
    }

    //
    // GET: /Account/Login
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
      var vm = await _account.BuildLoginViewModelAsync(returnUrl);

    //   if (vm.IsExternalLoginOnly)
    //   {
    //     // only one option for logging in
    //     return ExternalLogin(vm.ExternalProviders.First().AuthenticationScheme, returnUrl);
    //   }

      return View(vm);
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginInputModel model)
    {
      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberLogin, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          _logger.LogInformation(1, "User logged in.");
          return RedirectToLocal(model.ReturnUrl);
        }
        if (result.IsLockedOut)
        {
          _logger.LogWarning(2, "User account locked out.");
          return View("Lockout");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Invalid login attempt.");
          return View(await _account.BuildLoginViewModelAsync(model));
        }
      }

      // If we got this far, something failed, redisplay form
      return View(await _account.BuildLoginViewModelAsync(model));
    }

    /// <summary>
    /// Show logout page
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
      var vm = await _account.BuildLogoutViewModelAsync(logoutId);
      return await Logout(vm);
    }

    /// <summary>
    /// Handle logout page postback
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(LogoutViewModel model)
    {
      var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);
      if (vm.TriggerExternalSignout)
      {
        string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
        try
        {
          // hack: try/catch to handle social providers that throw
          await HttpContext.Authentication.SignOutAsync(vm.ExternalAuthenticationScheme,
              new AuthenticationProperties { RedirectUri = url });
        }
        catch (NotSupportedException) // this is for the external providers that don't have signout
        {
        }
        catch (InvalidOperationException) // this is for Windows/Negotiate
        {
        }
      }

      // delete authentication cookie
      await _signInManager.SignOutAsync();

      return Redirect("/");
    }

    //
    // POST: /Account/ExternalLogin
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]// Copyright © 2014-present Kriasoft, LLC. All rights reserved.
                              // This source code is licensed under the MIT license found in the
                              // LICENSE.txt file in the root directory of this source tree.
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
      // Request a redirect to the external login provider.
      var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
      var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
      return Challenge(properties, provider);
    }

    //
    // GET: /Account/ExternalLoginCallback
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
      if (remoteError != null)
      {
        ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
        return View(nameof(Login));
      }
      var info = await _signInManager.GetExternalLoginInfoAsync();
      if (info == null)
      {
        return RedirectToAction(nameof(Login));
      }

      // Sign in the user with this external login provider if the user already has a login.
      var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
      if (result.Succeeded)
      {
        _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
        return RedirectToLocal(returnUrl);
      }
      if (result.IsLockedOut)
      {
        return View("Lockout");
      }
      else
      {
        // If the user does not have an account, then ask the user to create an account.
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["LoginProvider"] = info.LoginProvider;
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
      }
    }

    //
    // GET: /Account/Register
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      if (ModelState.IsValid)
      {
        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
          // Send an email with this link
          //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
          //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
          //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
          await _signInManager.SignInAsync(user, isPersistent: false);
          _logger.LogInformation(3, "User created a new account with password.");
          return RedirectToLocal(returnUrl);
        }
        AddErrors(result);
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // POST: /Account/ExternalLoginConfirmation
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
    {
      if (ModelState.IsValid)
      {
        // Get the information about the user from the external login provider
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
          return View("ExternalLoginFailure");
        }
        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
          result = await _userManager.AddLoginAsync(user, info);
          if (result.Succeeded)
          {
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
            return RedirectToLocal(returnUrl);
          }
        }
        AddErrors(result);
      }

      ViewData["ReturnUrl"] = returnUrl;
      return View(model);
    }

    // GET: /Account/ConfirmEmail
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
      if (userId == null || code == null)
      {
        return View("Error");
      }
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return View("Error");
      }
      var result = await _userManager.ConfirmEmailAsync(user, code);
      return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }

    //
    // GET: /Account/ForgotPassword
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
      return View();
    }

    //
    // POST: /Account/ForgotPassword
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
          // Don't reveal that the user does not exist or is not confirmed
          return View("ForgotPasswordConfirmation");
        }

        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        // Send an email with this link
        //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
        //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
        //return View("ForgotPasswordConfirmation");
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // GET: /Account/ForgotPasswordConfirmation
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
      return View();
    }

    //
    // GET: /Account/ResetPassword
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string code = null)
    {
      return code == null ? View("Error") : View();
    }

    //
    // POST: /Account/ResetPassword
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var user = await _userManager.FindByNameAsync(model.Email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
      }
      var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
      }
      AddErrors(result);
      return View();
    }

    //
    // GET: /Account/ResetPasswordConfirmation
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
      return View();
    }

    #region Helpers

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }

    private Task<User> GetCurrentUserAsync()
    {
      return _userManager.GetUserAsync(HttpContext.User);
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction(nameof(HomeController.Index), "Home");
      }
    }

    #endregion
  }
}
