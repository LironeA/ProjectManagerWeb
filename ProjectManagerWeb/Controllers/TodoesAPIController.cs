using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagerWeb.Models;

namespace ProjectManagerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesAPIController : ControllerBase
    {
        private readonly MyProjectManagerDBContext _context;

        public TodoesAPIController(MyProjectManagerDBContext context)
        {
            _context = context;
        }

        // GET: api/TodoesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            if (_context.Todo == null)
            {
                return NotFound();
            }
            return await _context.Todo.ToListAsync();
        }

        // GET: api/TodoesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            if (_context.Todo == null)
            {
                return NotFound();
            }
            var todo = await _context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/TodoesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            if (!_context.Projects.Any(x => x.Id == todo.ProjectId))
            {
                return NotFound();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/TodoesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            if (_context.Todo == null)
            {
                return Problem("Entity set 'MyProjectManagerDBContext.Todo'  is null.");
            }
            if (!_context.Projects.Any(x => x.Id == todo.ProjectId))
            {
                return NotFound();
            }
            if(todo.Id != 0)
            {
                return BadRequest();
            }
            if(String.IsNullOrEmpty(todo.Name))
            {
                return BadRequest("Name is required.");
            }
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/TodoesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            if (_context.Todo == null)
            {
                return NotFound();
            }
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TodoExists(int id)
        {
            return (_context.Todo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
