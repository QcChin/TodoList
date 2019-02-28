using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Services;
using TodoList.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TodoList.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITodoService _todoService;

        public HomeController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        private Guid UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                    return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Guid.Empty;
            }
        }
        
        public async Task<IActionResult> Index(int? start, int? pageSize)
        {
            var _start = start ?? 0;
            var _pageSzie = pageSize ?? 10;
            if (_pageSzie == 0)
                return View();
            var todos = await _todoService.GetTodosAsync(_start, _pageSzie, UserId);
            return View(todos);
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
