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
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var todoTypes = await _todoService.GetAllTodoTypesAsync();
            if (todoTypes.Count() == 0)
                return Redirect("/TodoType/Create?returnUrl=/Todo/Create");
            ViewBag.TodoTypes = todoTypes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo,Guid TodoType)
        {
            if(ModelState.IsValid)
            {
                todo.AddModel(UserId);
                var _todoType = await _todoService.FindTodoTypeByIdAsync(TodoType);
                todo.TodoType = _todoType;
                var result = await _todoService.CreateTodoAsync(todo);
                if (result)
                  return Redirect("/");
                return RedirectToAction("Index");
            }
            else
                return View(todo);
        }


        public async Task<IActionResult> Modify(Guid id)
        {
            var todo = await _todoService.FindTodoByIdAsync(id);
            if(todo != null)
            {
                var todoTypes = await _todoService.GetAllTodoTypesAsync();
                ViewBag.TodoTypes = todoTypes;
                return View(todo);
            }
            return Redirect("/home/error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(Todo todo)
        {
            if(ModelState.IsValid)
            {
                todo.UpdateModel(UserId);
                var result = await _todoService.UpdateTodoAsync(todo);
                if (result)
                    return Redirect("/home/index");
                return RedirectToAction("Modify", new { id = todo.Id });
            }
            return View();
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _todoService.GetTodoItemsAsync(id);
            ViewBag.TodoId = id;
            return View(result);
        }

        public async Task<IActionResult> Finish(Guid id)
        {
            return await SetTodoStatus(id, TodoStatus.已完成);
        }

        public async Task<IActionResult> SetImportant(Guid id)
        {
            return await SetTodoStatus(id, TodoStatus.重要);
        }

        public async Task<IActionResult> UnFinish(Guid id)
        {
            return await SetTodoStatus(id, TodoStatus.代办);
        }

        private async Task<IActionResult> SetTodoStatus(Guid id,TodoStatus status)
        {
            var todo = await _todoService.FindTodoByIdAsync(id);
            if (todo == null)
                return Redirect("/home/index");
            todo.Status = TodoStatus.已完成;
            todo.UpdateModel(UserId);
            var result = await _todoService.UpdateTodoAsync(todo);
            if (!result)
                return Redirect("/home/index");
            return Redirect("/");
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _todoService.DelTodoAsync(id);
            if (!result)
                return Redirect("/home/index");
            return Redirect("/");
        }
    }
}