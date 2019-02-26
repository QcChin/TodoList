using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain.Models;

namespace TodoList.Domain.Interfaces
{
    public interface ITodoTypeRepository : IAsyncRepository<TodoType>
    {
    }
}
