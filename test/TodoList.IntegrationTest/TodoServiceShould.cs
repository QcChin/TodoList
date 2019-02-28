using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Models;
using TodoList.Domain.Services;
using TodoList.Repository.Data;
using TodoList.Web.Data;
using Xunit;

namespace TodoList.IntegrationTest
{
    public class TodoServiceShould
    {
        private readonly ITodoService _todoService;
        private readonly DbContextOptions<TodoContext> options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "Test_Todo").Options;
        private TodoContext _inMemoryContext;
        public TodoServiceShould()
        {
            _inMemoryContext = new TodoContext(options);
            var todoRepository = new TodoRepository(_inMemoryContext);
            var todoItemRepository = new TodoItemRepository(_inMemoryContext);
            var todoTypeRepository = new TodoTypeRepository(_inMemoryContext);
            _todoService = new TodoService(todoRepository, todoTypeRepository, todoItemRepository);
        }

        [Fact]
        public async Task CreateTodo()
        {
            var fakeUserid = Guid.NewGuid();
            var fakeType = new TodoType
            {
                Name = "活动"
            };
            fakeType.AddModel(fakeUserid);
            var fakeTodo = new Todo
            {
                Title = "创建明日社区活动",
                TodoType = fakeType,
                OffTime = DateTime.Now
            };
            fakeTodo.AddModel(fakeUserid);
            await _todoService.CreateTodoAsync(fakeTodo);
            var title = await _inMemoryContext.Todos.Where(c => c.Title == fakeTodo.Title).Select(c=>c.Title).FirstOrDefaultAsync();
            Assert.Equal(1, await _inMemoryContext.Todos.Where(c => c.Id == fakeTodo.Id).CountAsync());
            Assert.False(string.IsNullOrEmpty(title));
        }

        [Fact]
        public async Task UpdateTodo()
        {
            var fakeUserid = Guid.NewGuid();
            var fakeType = new TodoType
            {
                Name = "活动"
            };
            fakeType.AddModel(fakeUserid);
            var fakeTodo = new Todo
            {
                Title = "更新明日社区活动",
                TodoType = fakeType,
                OffTime = DateTime.Now
            };
            fakeTodo.AddModel(fakeUserid);
            _inMemoryContext.Add<TodoType>(fakeType);
            _inMemoryContext.Add<Todo>(fakeTodo);
            await _inMemoryContext.SaveChangesAsync();
            fakeTodo.Title = "更新明日团队活动1";
            fakeTodo.UpdateModel(fakeUserid);
            await _todoService.UpdateTodoAsync(fakeTodo);
            var title = (await _inMemoryContext.Todos.FindAsync(fakeTodo.Id)).Title;
            Assert.Equal("更新明日团队活动1", title);
        }

        [Fact]
        public async Task DelTodo()
        {
            var fakeUserid = Guid.NewGuid();
            var fakeType = new TodoType
            {
                Name = "活动"
            };
            fakeType.AddModel(fakeUserid);
            var fakeTodo = new Todo
            {
                Title = "明日社区活动",
                TodoType = fakeType,
                OffTime = DateTime.Now
            };
            fakeTodo.AddModel(fakeUserid);
            _inMemoryContext.Add<Todo>(fakeTodo);
            await _inMemoryContext.SaveChangesAsync();
            Assert.Equal(1, await _inMemoryContext.Todos.Where(c => c.Id == fakeTodo.Id).Where(c => c.IsActive == true).CountAsync());
            await _todoService.DelTodoAsync(fakeTodo.Id);
            Assert.Equal(1, await _inMemoryContext.Todos.Where(c => c.Id == fakeTodo.Id).Where(c => c.IsActive == false).CountAsync());
        }
    }
}
