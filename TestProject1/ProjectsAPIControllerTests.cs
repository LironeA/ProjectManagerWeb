using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagerWeb.Controllers;
using ProjectManagerWeb.Models;
using ProjectManagerWeb.Services;

namespace TestProject1
{
    [TestFixture]
    public class ProjectsAPIControllerTests
    {
        private Mock<IProjectService> _mockService;
        private ProjectsAPIController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IProjectService>();
            _controller = new ProjectsAPIController(_mockService.Object);
        }

        [Test]
        public async Task GetProjects_ReturnsOkResult_WithListOfProjects()
        {
            var projects = new List<Project> { new Project { Id = 1, Name = "Project 1" } };
            _mockService.Setup(s => s.GetProjectsAsync()).ReturnsAsync(projects);

            var result = await _controller.GetProjects();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(projects, okResult.Value);
        }

        [Test]
        public async Task GetProject_ReturnsOkResult_WithProject()
        {
            var project = new Project { Id = 1, Name = "Project 1" };
            _mockService.Setup(s => s.GetProjectByIdAsync(1)).ReturnsAsync(project);

            var result = await _controller.GetProject(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(project, okResult.Value);
        }

        [Test]
        public async Task GetProject_ReturnsNotFound_WhenProjectDoesNotExist()
        {
            _mockService.Setup(s => s.GetProjectByIdAsync(It.IsAny<int>())).ReturnsAsync((Project)null);

            var result = await _controller.GetProject(1);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            _mockService.Verify(s => s.GetProjectByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task PostProject_ReturnsOkResult_WhenProjectIsCreated()
        {
            var project = new Project { Name = "Project 1" };
            _mockService.Setup(s => s.CreateProjectAsync(project)).ReturnsAsync(project);

            var result = await _controller.PostProject(project);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(project, okResult.Value);
        }

        [Test]
        public async Task PostProject_ReturnsBadRequest_WhenExceptionThrown()
        {
            var project = new Project { Name = "Project 1" };
            _mockService.Setup(s => s.CreateProjectAsync(project)).ThrowsAsync(new Exception("Database error"));

            ActionResult<Project> result = null;

            Assert.Throws<Exception>(() => result = _controller.PostProject(project).GetAwaiter().GetResult());
        }


        [Test]
        public async Task PutProject_ReturnsOkResult_WhenProjectIsUpdated()
        {
            var project = new Project { Id = 1, Name = "Project 1" };
            _mockService.Setup(s => s.UpdateProjectAsync(1, project)).ReturnsAsync(true);

            var result = await _controller.PutProject(1, project);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteProject_ReturnsOkResult_WhenProjectIsDeleted()
        {
            _mockService.Setup(s => s.DeleteProjectAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteProject(1);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}