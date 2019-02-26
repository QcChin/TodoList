using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Models;
using TodoList.Domain.Interfaces;

namespace TodoList.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly IAsyncRepository<Todo> _todoRepository;
        private readonly IAsyncRepository<TodoType> _todoTypeRepository;
        private readonly IAsyncRepository<TodoItem> _todoItemRepository;

        public TodoService(IAsyncRepository<Todo> todoRepository,
            IAsyncRepository<TodoType> todoTypeRepository,
            IAsyncRepository<TodoItem> todoItemRepository)
        {
            _todoRepository = todoRepository;
            _todoTypeRepository = todoTypeRepository;
            _todoItemRepository = todoItemRepository;
        }

        public async Task<bool> CreateTodoAsync(Todo todo)
        {
            return await _todoRepository.AddAsync(todo);
        }

        public async Task<bool> CreateTodoItemAsync(TodoItem todoitem)
        {
            return await _todoItemRepository.AddAsync(todoitem);
        }

        public async Task<bool> CreateTodoTypeAsync(TodoType todoType)
        {
            return await _todoTypeRepository.AddAsync(todoType);
        }

        public async Task<bool> DelTodoAsync(Guid id)
        {
            return await _todoRepository.DeleteAsync(id);
        }

        public async Task<bool> DelTodoItemAsync(Guid id)
        {
            return await _todoItemRepository.DeleteAsync(id);
        }

        public async Task<bool> DelTodoTypeAsync(Guid id)
        {
            return await _todoTypeRepository.DeleteAsync(id);
        }

        public Task<Todo> FindTodoByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> FindTodoItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoType>> FindTodoTypeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> GetTodoItemsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Todo>> GetTodos(int start, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoType>> GetTodoTypesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTodoAsync(Todo todo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTodoItemAsynC(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTodoTypeAsynC(TodoType todoItem)
        {
            throw new NotImplementedException();
        }
    }
}
