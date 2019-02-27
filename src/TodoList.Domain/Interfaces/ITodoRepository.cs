using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Models;

namespace TodoList.Domain.Interfaces
{
    public interface ITodoRepository : IAsyncRepository<Todo>
    {
        Task<IReadOnlyList<Todo>> GetTodosAsync(ISpecification<Todo> spec);
    }
}
