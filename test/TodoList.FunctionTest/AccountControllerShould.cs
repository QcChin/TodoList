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
    public class AccountControllerShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        public HttpClient Client { get; private set; }
        public AccountControllerShould(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            Client = _webApplicationFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task ReturnsSignInScreenOnGet()
        {
            var response = await Client.GetAsync("/identity/account/login");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("TodoList.Web", stringResponse);
        }
        

        [Fact]
        public async Task RegisterFakeAccount()
        {
            var getResponse = await Client.GetAsync("/identity/account/register");
            getResponse.EnsureSuccessStatusCode();
            var stringGetResponse = await getResponse.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(stringGetResponse));
            Match match = Regex.Match(stringGetResponse, @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
            var _requestVerificationToken = match.Success ? match.Groups[1].Captures[0].Value : null;
            //var content = 
            //Client.PostAsync();
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
