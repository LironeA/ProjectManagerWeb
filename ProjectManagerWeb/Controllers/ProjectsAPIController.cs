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
    public class ProjectsAPIController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsAPIController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/ProjectsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();
            if (projects == null)
            {
                return NotFound();
            }
            return Ok(projects);
        }

        // GET: api/ProjectsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/ProjectsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            var updated = await _projectService.UpdateProjectAsync(id, project);
            if (!updated)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/ProjectsAPI
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            var createdProject = await _projectService.CreateProjectAsync(project);
            if (createdProject == null)
            {
                return BadRequest();
            }

            return Ok(createdProject);
        }

        // DELETE: api/ProjectsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleted = await _projectService.DeleteProjectAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
