using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models;
using TodoList.Repository.Data;
using Xunit;

namespace TodoList.IntegrationTest
{
    public class RepositoryShould
    {

        [Fact]
        public async Task CreateTest()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                            .UseInMemoryDatabase(databaseName: "Test_Todo").Options;
        }
    }
}
