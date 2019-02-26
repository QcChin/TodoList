using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;

namespace TodoList.Repository.Data
{
    public class TodoTypeRepository : EfRepository<TodoType>, ITodoTypeRepository
    {
        public TodoTypeRepository(TodoContext dbContext) : base(dbContext)
        { }
    }
}
