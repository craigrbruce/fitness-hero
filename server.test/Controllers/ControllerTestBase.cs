using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Server.Models.Core;
using Server.Persistence;

namespace Server.Test.Controllers
{
    public class ControllerTestsBase<TEntity>
           where TEntity : Model
    {

        protected Mock<IRepository<TEntity>> Repository = new Mock<IRepository<TEntity>>();
        protected Mock<HttpContext> Context;
        private Controller _controller;
        protected Controller Controller
        {
            get { return _controller; }
            set
            {
                _controller = value;
                _controller.ControllerContext = new ControllerContext(new ActionContext
                {
                    HttpContext = Context.Object,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ControllerActionDescriptor()
                });
            }
        }

        public ControllerTestsBase()
        {
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Path).Returns(new PathString("/SomeValue"));
            Context = new Mock<HttpContext>();
            Context.SetupGet(x => x.Request).Returns(request.Object);
            Context.SetupGet(x => x.User).Returns(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(new List<Claim>
                {
                    // TODO create a fake user id for testing
                })
            }));
        }
    }
}
