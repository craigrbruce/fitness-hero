using Xunit;
using Server.Models;
using FluentAssertions;
using Moq;
using Server.Controllers.Api.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Server.Test.Controllers.Api.Clients
{
  public class ClientsControllerTest : ControllerTestsBase<Client>
  {
    public ClientsControllerTest()
    {
      Controller = new ClientsController(Repository.Object);
    }

    [Fact]
    public void when_post_is_called_should_invoke_save_and_return_created_result()
    {
      var client = new Client();
      Repository.Setup(x => x.Save(client)).Returns(client);
      var result = (Controller as ClientsController)?.Post(client);

      Repository.Verify(x => x.Save(client), Times.Once);
      result.Should().BeOfType<CreatedResult>();
    }

    [Fact]
    public void when_put_is_called_should_invoke_update_and_return_json()
    {
      var client = new Client { Id = 1 };
      Repository.Setup(x => x.Get(It.IsAny<int>())).Returns(client);
      var result = (Controller as ClientsController)?.Put(1, client);

      Repository.Verify(x => x.Update(client), Times.Once);
      result.Should().BeOfType<JsonResult>();
    }

    [Fact]
    public void when_put_is_called_and_client_is_not_found_should_return_not_found_result()
    {
      var client = new Client { Id = 1 };
      Repository.Setup(x => x.Get(It.IsAny<int>())).Returns<Client>(null);
      var result = (Controller as ClientsController)?.Put(4, client);

      result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void when_delete_is_called_should_invoke_delete_and_return_no_content_result()
    {
      var result = (Controller as ClientsController)?.Delete(1);
      
      Repository.Verify(x => x.Delete(new[] { 1 }), Times.Once);
      result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public void when_bulk_delete_is_called_should_return_no_content_result()
    {
      var result = (Controller as ClientsController)?.BulkDelete(new[] { 1, 2, 3 });

      Repository.Verify(x => x.Delete(new[] { 1, 2, 3 }), Times.Once);
      result.Should().BeOfType<NoContentResult>();
    }
  }
}
