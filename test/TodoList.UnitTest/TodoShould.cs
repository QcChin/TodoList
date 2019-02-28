using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;
using TodoList.Domain.Services;
using Xunit;

namespace TodoList.UnitTest
{
    public class TodoShould
    {
        private Mock<ITodoRepository> _mockTodoRepository;
        private Mock<IAsyncRepository<TodoType>> _mockTodoTypeRepository;
        private Mock<IAsyncRepository<TodoItem>> _mockTodoItemRepository;

        public TodoShould()
        {
            _mockTodoRepository = new Mock<ITodoRepository>();
            _mockTodoTypeRepository = new Mock<IAsyncRepository<TodoType>>();
            _mockTodoItemRepository = new Mock<IAsyncRepository<TodoItem>>();
        }

        [Fact]
        public async Task AddTodoAsync()
        {
            var fakeUserId = Guid.NewGuid();
            var todo = new Todo { Title = "测试标题", OffTime = DateTime.Now };
            todo.AddModel(fakeUserId);
            _mockTodoRepository.Setup(x => x.AddAsync(todo))
                .ReturnsAsync(true);

            var todoService = new TodoService(_mockTodoRepository.Object, null, null);
            var result = await todoService.CreateTodoAsync(todo);
            Assert.True(result);
        }

        [Fact]
        public async Task ModifyTodo()
        {
            var fakeUserId = Guid.NewGuid();
            var todo = new Todo { Title = "测试标题", OffTime = DateTime.Now };
            todo.AddModel(fakeUserId);
            _mockTodoRepository.Setup(x => x.UpdateAsync(It.IsAny<Todo>()))
                .ReturnsAsync(true);
            var todoService = new TodoService(_mockTodoRepository.Object, null, null);
            var result = await todoService.UpdateTodoAsync(todo);
            Assert.True(result);
        }
    }

    
}
