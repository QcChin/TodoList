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
    public abstract class EfRepository<T> : IAsyncRepository<T> where T : Entity
    {
        protected readonly TodoContext _dbContext;
        public EfRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return (await _dbContext.SaveChangesAsync()) == 1;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.FindAsync<Entity>(id);
            if (entity == null)
                return false;
            entity.IsActive = false;
            _dbContext.Entry(entity).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return (await _dbContext.SaveChangesAsync()) == 1;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
