//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using TodoList.Domain.Models;
//using TodoList.Domain.Services;
//using TodoList.Web.Data;
//using Xunit;

//namespace TodoList.IntegrationTest
//{
//    public class TodoServiceShould
//    {
//        [Fact]
//        public async Task CreateTodo()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "Test_Todo").Options;


//            using (var inMemoryContext = new ApplicationDbContext(options))
//            {
//                var service = ITodoService.;
//                var fakeUserid = Guid.NewGuid();
//                var fakeType = new TodoType
//                {
//                    Name = "活动"
//                };
//                fakeType.AddModel(fakeUserid);
//                var fakeTodo = new Todo
//                {
//                    Title = "明日社区活动",
//                    TodoType = fakeType,
//                    OffTime = DateTime.Now
//                };
//                fakeTodo.AddModel(fakeUserid);
//                await service.CreateTodoAsync(fakeTodo);
//                var title = await inMemoryContext.Todo.Select(c => c.Title).FirstOrDefaultAsync();
//                Assert.Equal(1, await inMemoryContext.Todo.CountAsync());
//                Assert.False(string.IsNullOrEmpty(title));
//                Assert.NotEqual(default(DateTime), await inMemoryContext.Todo.Select(c => c.OffTime).FirstOrDefaultAsync());
//                Assert.NotEqual(default(Guid), await inMemoryContext.Todo.Select(c => c.Creator).FirstOrDefaultAsync());
//                Assert.NotEqual(default(Guid), await inMemoryContext.Todo.Select(c => c.Id).FirstOrDefaultAsync());
//                Assert.NotEqual(default(Guid), await inMemoryContext.Todo.Select(c => c.TodoType.Id).FirstOrDefaultAsync());
//            }
//        }

//    }
//}
