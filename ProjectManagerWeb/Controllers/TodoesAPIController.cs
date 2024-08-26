using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagerWeb.Models;
using ProjectManagerWeb.Services;

namespace ProjectManagerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesAPIController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoesAPIController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/TodoesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            var todos = await _todoService.GetTodosAsync();
            if (todos == null)
            {
                return NotFound();
            }
            return Ok(todos);
        }

        // GET: api/TodoesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/TodoesAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            var updated = await _todoService.UpdateTodoAsync(id, todo);
            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/TodoesAPI
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            var createdTodo = await _todoService.CreateTodoAsync(todo);
            if (createdTodo == null)
            {
                return BadRequest();
            }

            return Ok(createdTodo);
        }

        // DELETE: api/TodoesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var deleted = await _todoService.DeleteTodoAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
