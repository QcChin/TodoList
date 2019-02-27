using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;
using TodoList.Repository.Data;
using Xunit;
using Xunit.Extensions;

namespace TodoList.IntegrationTest
{
    public class RepositoryShould
    {

        [Fact]
        public async Task CreateTest()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                            .UseInMemoryDatabase(databaseName: "Test_Todo").Options;
            using (var inMemoryContext = new TodoContext(options))
            {
                var todo = new Todo();
                var todoRepository = new EfRepository<Todo>(inMemoryContext);
                Assert.True(await todoRepository.AddAsync(todo));
            }
        }

        private IEnumerable<Type> GetAllTypesIsSubclassOfEntity()
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                             from type in assembly.GetTypes()
                             where type.IsSubclassOf(typeof(Entity))
                             select type;
        }
    }
}
