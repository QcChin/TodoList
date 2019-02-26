using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Models;

namespace TodoList.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
