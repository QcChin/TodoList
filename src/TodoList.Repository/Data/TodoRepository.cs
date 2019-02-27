using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;

namespace TodoList.Repository.Data
{
    public class TodoRepository : EfRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoContext dbContext) : base(dbContext)
        {}

        public async Task<IReadOnlyList<Todo>> GetTodosAsync(ISpecification<Todo> spec)
        {
            return await SpecificationEvaluator<Todo>.GetQuery(_dbContext.Set<Todo>().Include(c => c.TodoType).AsQueryable(), spec).ToListAsync();
        }
    }
}
