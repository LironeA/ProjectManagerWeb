using Microsoft.EntityFrameworkCore;
using ProjectManagerWeb.Models;

namespace ProjectManagerWeb.Services
{

    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task<bool> UpdateTodoAsync(int id, Todo todo);
        Task<Todo> CreateTodoAsync(Todo todo);
        Task<bool> DeleteTodoAsync(int id);
        bool TodoExists(int id);
        bool ProjectExists(int projectId);
    }

    public class TodoService : ITodoService
    {
        private readonly MyProjectManagerDBContext _context;

        public TodoService(MyProjectManagerDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            return await _context.Todo.ToListAsync();
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Todo.FindAsync(id);
        }

        public async Task<bool> UpdateTodoAsync(int id, Todo todo)
        {
            if (id != todo.Id || !ProjectExists(todo.ProjectId))
            {
                return false;
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
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            if (todo == null || todo.Id != 0 || !ProjectExists(todo.ProjectId) || string.IsNullOrEmpty(todo.Name))
            {
                return null;
            }

            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return false;
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }

        public bool ProjectExists(int projectId)
        {
            return _context.Projects.Any(x => x.Id == projectId);
        }
    }
}
