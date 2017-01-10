using System.Net.Http;
using FluentAssertions;
using Xunit;

namespace FH.Test.Integration.Api.User
{
    public class UserTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;

        public UserTests(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void endpoint_should_exist()
        {
            // TODO .. CallApi<object> should be CallApi<User>
            var response =
                _fixture.CallApi<object>("http://localhost/api/v1/users", HttpMethod.Get).Result;
            response.Should().NotBeNull();
        }
    }
}
