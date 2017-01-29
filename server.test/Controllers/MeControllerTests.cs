using Xunit;
using System.Collections.Generic;
using Server.Models;
using Server.Models.Core;
using FluentAssertions;
using Moq;
using Server.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Test.Controllers.Api.Clients
{
  public class MeControllerTests : ControllerTestsBase<Model>
  {
    private Mock<UserManager<User>> UserManager;
    private Mock<IUserStore<User>> _userStore = new Mock<IUserStore<User>>();
    public MeControllerTests()
    {
      var options = new Mock<IOptions<IdentityOptions>>();
      var hasher = new Mock<IPasswordHasher<User>>();
      var lookup = new Mock<ILookupNormalizer>();
      var provider = new Mock<System.IServiceProvider>();
      var logger = new Mock<ILogger<UserManager<User>>>();
      var userManager = new UserManager<User>(
            _userStore.Object,
            options.Object,
            hasher.Object,
            new List<IUserValidator<User>>(),
            new List<IPasswordValidator<User>>(),
            lookup.Object,
            new IdentityErrorDescriber(),
            provider.Object,
            logger.Object
          );

      Controller = new MeController(userManager);
    }

    [Fact]
    public void should_return_forbid_result_if_user_not_found()
    {
      var result = (Controller as MeController)?.Get().Result;
      result.Should().BeOfType<ForbidResult>();
    }

    [Fact(Skip="Mocking UserManager is prohibitively hard, make a call as to the usefulness of testing this")]
    public void should_return_user_view_model_if_user_found()
    {
      _userStore
        .Setup(
                x => (x as UserManager<User>)
                .GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(new Task<User>(() => new User { Email = "some email" }));


    }
  }
}
