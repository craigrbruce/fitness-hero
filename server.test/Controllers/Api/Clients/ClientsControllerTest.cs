using Xunit;
using Server.Models;
using Server.Controllers.Api.Clients;

namespace Server.Test.Controllers.Api.Clients{
    public class ClientsControllerTest : ControllerTestsBase<Client> {
        public ClientsControllerTest()
        {
            Controller = new ClientsController(Repository.Object);
        }

        [Fact]
        public void should_foo_bar() {
            // test this fucker

        }
        
    }
    
}
