using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain.Models;
using Xunit;

namespace TodoList.UnitTest
{
    public class EntityShould
    {
        private Guid _userId = Guid.NewGuid();

        [Fact]
        public void EntityAddModel()
        {
            var todo = new Todo();
            todo.AddModel(_userId);

            Assert.Equal(_userId, todo.Creator);
            Assert.NotEqual(default(DateTime), todo.CreateDate);
        }
    }
}
