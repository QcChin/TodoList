using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain.Models;

namespace TodoList.Repository.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {}

        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoType> TodoTypes { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
