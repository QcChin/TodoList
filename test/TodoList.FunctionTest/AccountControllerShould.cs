using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TodoList.Web;
using Xunit;

namespace TodoList.FunctionTest
{
    public class AccountControllerShould : IClassFixture<TestFixture>
    {
        public HttpClient Client { get; }
        public AccountControllerShould(TestFixture fixturr)
        {
            Client = fixturr.Client;
        }

        [Fact]
        public async Task ReturnsSignInScreenOnGet()
        {
            var response = await Client.GetAsync("/identity/account/login");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("demouser@microsoft.com", stringResponse);
        }

        [Fact]
        public async Task RegexMatchesValidRequestVerificationToken()
        {
            // TODO: Move to a unit test
            // TODO: Move regex to a constant in test project
            var input = @"<input name=""__RequestVerificationToken"" type=""hidden"" value=""CfDJ8Obhlq65OzlDkoBvsSX0tgxFUkIZ_qDDSt49D_StnYwphIyXO4zxfjopCWsygfOkngsL6P0tPmS2HTB1oYW-p_JzE0_MCFb7tF9Ol_qoOg_IC_yTjBNChF0qRgoZPmKYOIJigg7e2rsBsmMZDTdbnGo"" /><input name=""RememberMe"" type=""hidden"" value=""false"" /></form>";
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(input);
            var group = match.Groups.LastOrDefault();
            Assert.NotNull(group);
            Assert.True(group.Value.Length > 50);
        }
    }
}
