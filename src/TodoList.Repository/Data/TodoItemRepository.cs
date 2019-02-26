using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;

namespace TodoList.Repository.Data
{
    public class TodoItemRepository : EfRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext dbContext) : base(dbContext)
        { }
    }
}
