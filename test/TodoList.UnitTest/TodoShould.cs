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
        private Mock<IAsyncRepository<Todo>> _mockTodoRepository;
        private Mock<IAsyncRepository<TodoType>> _mockTodoTypeRepository;
        private Mock<IAsyncRepository<TodoItem>> _mockTodoItemRepository;

        public TodoShould()
        {
            _mockTodoRepository = new Mock<IAsyncRepository<Todo>>();
            _mockTodoRepository = new Mock<IAsyncRepository<Todo>>();
            _mockTodoItemRepository = new Mock<IAsyncRepository<TodoItem>>();
        }

        [Fact]
        public async Task AddTodoAsync()
        {
            var todo = new Todo();
            _mockTodoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(todo);

            var todoService = new TodoService(_mockTodoRepository.Object, null, null);
            await todoService.CreateTodoAsync(todo);

        }
    }

    
}
