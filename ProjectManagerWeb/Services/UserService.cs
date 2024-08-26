using Microsoft.EntityFrameworkCore;
using ProjectManagerWeb.Models;

namespace ProjectManagerWeb.Services
{

    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, User user);
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        bool UserExists(int id);
    }

    public class UserService : IUserService
    {
        private readonly MyProjectManagerDBContext _context;

        public UserService(MyProjectManagerDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            _context.Projects.Load(); // If you need to load related data
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            if (id != user.Id)
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name))
            {
                return null;
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
