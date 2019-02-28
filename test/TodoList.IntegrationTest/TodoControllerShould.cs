using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Models;
using TodoList.Domain.Services;
using TodoList.Web.Controllers;
using Xunit;

namespace TodoList.IntegrationTest
{
    public class TodoControllerShould
    {

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            var mockService = new Mock<ITodoService>();
            var fakeUserId = Guid.NewGuid();

            var fakeUserClaims = new Mock<ClaimsPrincipal>();
            fakeUserClaims.Setup(c => c.Identity.IsAuthenticated).Returns(false);

            // Cant run, beacuse the ClaimTypes.NameIdentifier is a static class's propertity
            //fakeUserClaims.Setup(c => c.FindFirstValue(ClaimTypes.NameIdentifier)).Returns(fakeUserId.ToString());

            //mockService.Setup(service => service.GetTodosAsync(0, 10, fakeUserId))
            //    .ReturnsAsync(GetTestTodos(fakeUserId));

            var controller = new HomeController(mockService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = fakeUserClaims.Object
                    }
                }
            };

            mockService.Setup(service => service.GetTodosAsync(0, 10, Guid.Empty))
                .ReturnsAsync(GetTestTodos(Guid.Empty));

            // Act
            var result = await controller.Index(0, 10);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Todo>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Todo> GetTestTodos(Guid fakeUserid)
        {
            var fakeType = new TodoType
            {
                Name = "活动"
            };
            fakeType.AddModel(fakeUserid);
            var fakeTodo1 = new Todo
            {
                Title = "社区活动",
                TodoType = fakeType,
                OffTime = DateTime.Now
            };
            fakeTodo1.AddModel(fakeUserid);
            var fakeTodo2 = new Todo
            {
                Title = "游园活动",
                TodoType = fakeType,
                OffTime = DateTime.Now
            };
            fakeTodo2.AddModel(fakeUserid);
            var todos = new List<Todo>();
            todos.Add(fakeTodo1);
            todos.Add(fakeTodo2);
            return todos;
        }

    }
}
