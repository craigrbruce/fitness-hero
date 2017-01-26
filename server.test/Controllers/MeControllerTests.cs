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

namespace Server.Test.Controllers.Api.Clients
{
  public class MeControllerTests : ControllerTestsBase<Model>
  {
    private Mock<UserManager<User>> UserManager;
    public MeControllerTests()
    {
      var userStore = new Mock<IUserStore<User>>();
      var options = new Mock<IOptions<IdentityOptions>>();
      var hasher = new Mock<IPasswordHasher<User>>();
      var lookup = new Mock<ILookupNormalizer>();
      var provider = new Mock<System.IServiceProvider>();
      var logger = new Mock<ILogger<UserManager<User>>>();

      var userManager = new UserManager<User>(
          userStore.Object, options.Object,
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
      var result = (Controller as MeController)?.Get().ConfigureAwait(false);
      result.Should().BeOfType<ForbidResult>();
    }
  }
}
