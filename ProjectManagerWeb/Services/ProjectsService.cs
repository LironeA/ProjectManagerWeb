using Microsoft.EntityFrameworkCore;
using ProjectManagerWeb.Models;

namespace ProjectManagerWeb.Services
{

    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<bool> UpdateProjectAsync(int id, Project project);
        Task<Project> CreateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
        bool ProjectExists(int id);
    }


    public class ProjectService : IProjectService
    {
        private readonly MyProjectManagerDBContext _context;

        public ProjectService(MyProjectManagerDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<bool> UpdateProjectAsync(int id, Project project)
        {
            if (id != project.Id)
            {
                return false;
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        public async Task<Project> CreateProjectAsync(Project project)
        {
            if (project == null || project.Id != 0 || string.IsNullOrEmpty(project.Name))
            {
                return null;
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
