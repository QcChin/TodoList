using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Models;
using TodoList.Domain.Services;

namespace TodoList.Web.Controllers
{
    public class TodoTypeController : Controller
    {
        private readonly ITodoService _todoService;
        public TodoTypeController(ITodoService todoService)
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
        public IActionResult Create(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoType todoType,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                todoType.AddModel(UserId);
                var result = await _todoService.CreateTodoTypeAsync(todoType);
                return Redirect(returnUrl);
            }
            return View(todoType);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _todoService.DelTodoTypeAsync(id);
            if (!result)
                return Redirect("/home/index");
            return Redirect("/");
        }
    }
}