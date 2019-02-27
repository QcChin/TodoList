using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Models;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Specifications;
using System.Linq.Expressions;

namespace TodoList.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IAsyncRepository<TodoType> _todoTypeRepository;
        private readonly IAsyncRepository<TodoItem> _todoItemRepository;

        public TodoService(ITodoRepository todoRepository,
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
            var specification = new BaseSpecification<TodoType>(c => c.Name == todoType.Name);
            var _todoTypes = await _todoTypeRepository.GetAsync(specification);
            if (_todoTypes.Count > 0)
                return false;
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

        public async Task<Todo> FindTodoByIdAsync(Guid id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<TodoItem> FindTodoItemByIdAsync(Guid id)
        {
            return await _todoItemRepository.GetByIdAsync(id);
        }

        public async Task<TodoType> FindTodoTypeByIdAsync(Guid id)
        {
            return await _todoTypeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TodoType>> GetAllTodoTypesAsync()
        {
            return await _todoTypeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync(Guid id)
        {
            var specification = new BaseSpecification<TodoItem>(c => c.TodoId == id);
            return await _todoItemRepository.GetAsync(specification);
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(int start, int pageSize, Guid userId)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} 错误");
            var specification = new BaseSpecification<Todo>(c=> c.Creator == userId);
            specification.ApplyPaging(start,pageSize);
            return await _todoRepository.GetTodosAsync(specification);
        }


        //public Task<IEnumerable<TodoType>> GetTodoTypesAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> UpdateTodoAsync(Todo todo)
        {
            return await _todoRepository.UpdateAsync(todo);
        }

        public async Task<bool> UpdateTodoItemAsynC(TodoItem todoItem)
        {
            return await _todoItemRepository.UpdateAsync(todoItem);
        }

        public async Task<bool> UpdateTodoTypeAsynC(TodoType todoType)
        {
            return await _todoTypeRepository.UpdateAsync(todoType);
        }
    }
}
