using System.Net.Http;
using FluentAssertions;
using Xunit;
using System.Net;

namespace FH.Test.Integration.Api
{
  public class MeTests : IClassFixture<IntegrationTestFixture>
  {
    private readonly IntegrationTestFixture _fixture;

    public MeTests(IntegrationTestFixture fixture)
    {
      _fixture = fixture;
    }

    [Fact(Skip="We need to seed the database in Test env with users etc...")]
    public void user_should_be_returned()
    {
      var response = _fixture.CallApi<Server.Models.User, object>(
                            "http://localhost/api/v1/me",
                            HttpMethod.Get
                        ).Result;

      response.Result.Should().NotBeNull();
      response.HttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
      // parse response.Result to User and check against seeded data
    }
  }
}
