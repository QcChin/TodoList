using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;

namespace TodoList.Repository.Data
{
    public class TodoRepository : EfRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoContext dbContext) : base(dbContext)
        {}
    }
}
