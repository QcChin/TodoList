using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Models;

namespace TodoList.Domain.Services
{
    public interface ITodoService
    {
        #region Todo
        Task<bool> CreateTodoAsync(Todo todo);

        Task<bool> UpdateTodoAsync(Todo todo);

        Task<bool> DelTodoAsync(Guid id);

        Task<IEnumerable<Todo>> GetTodos(int start, int pageSize);

        Task<Todo> FindTodoByIdAsync(Guid id);
        #endregion

        #region TodoItem
        Task<bool> CreateTodoItemAsync(TodoItem todoitem);

        Task<bool> UpdateTodoItemAsynC(TodoItem todoItem);

        Task<bool> DelTodoItemAsync(Guid id);

        Task<IEnumerable<TodoItem>> GetTodoItemsAsync(Guid id);

        Task<IEnumerable<TodoItem>> FindTodoItemByIdAsync(Guid id);

        #endregion

        #region TodoType
        Task<bool> CreateTodoTypeAsync(TodoType todoType);

        Task<bool> UpdateTodoTypeAsynC(TodoType todoType);

        Task<bool> DelTodoTypeAsync(Guid id);

        Task<IEnumerable<TodoType>> GetTodoTypesAsync(Guid id);

        Task<IEnumerable<TodoType>> FindTodoTypeByIdAsync(Guid id);
        #endregion

    }
}
