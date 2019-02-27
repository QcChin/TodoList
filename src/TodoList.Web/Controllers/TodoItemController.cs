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
    public class TodoItemController : Controller
    {
        private readonly ITodoService _todoService;
        public TodoItemController(ITodoService todoService)
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

        public async Task<IActionResult> Create(Guid todoId)
        {
            ViewBag.TodoId = todoId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if(ModelState.IsValid)
            {
                todoItem.AddModel(UserId);
                var result = await _todoService.CreateTodoItemAsync(todoItem);
                if (result)
                    return Redirect($"/Todo/Detail?id={todoItem.TodoId}");
                return RedirectToAction("Create", new { todoId = todoItem.TodoId });
            }
            return Redirect("/home/error");
        }

        public async Task<IActionResult> Modify(Guid id)
        {
            var todoItem = await _todoService.FindTodoItemByIdAsync(id);
            if (todoItem != null)
                return View(todoItem);
            return Redirect("/home/error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.UpdateModel(UserId);
                var result = await _todoService.UpdateTodoItemAsynC(todoItem);
                if (result)
                    return Redirect($"/todo/detail?id={todoItem.TodoId}");
                return Redirect("/home/index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _todoService.DelTodoItemAsync(id);
            if (!result)
                return Redirect("/home/index");
            return Redirect("/");
        }
    }
}