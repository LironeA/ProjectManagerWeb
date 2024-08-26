using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerWeb.Controllers;
using ProjectManagerWeb.Models;
using ProjectManagerWeb.Services;

namespace TestProject1
{
    [TestFixture]
    public class TodoesAPIControllerTests
    {
        private Mock<ITodoService> _mockService;
        private TodoesAPIController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<ITodoService>();
            _controller = new TodoesAPIController(_mockService.Object);
        }

        [Test]
        public async Task GetTodo_ReturnsOkResult_WithListOfTodos()
        {
            var todos = new List<Todo> { new Todo { Id = 1, Name = "Todo 1" } };
            _mockService.Setup(s => s.GetTodosAsync()).ReturnsAsync(todos);

            var result = await _controller.GetTodo();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(todos, okResult.Value);
        }

        [Test]
        public async Task GetTodo_ThrowsException_ReturnsInternalServerError()
        {
            _mockService.Setup(s => s.GetTodosAsync()).ThrowsAsync(new System.Exception("Test exception"));

            ActionResult<IEnumerable<Todo>> result = null;

            Assert.Throws<Exception>(() => result = _controller.GetTodo().GetAwaiter().GetResult());
        }

        [Test]
        public async Task GetTodo_ReturnsOkResult_WithTodo()
        {
            var todo = new Todo { Id = 1, Name = "Todo 1" };
            _mockService.Setup(s => s.GetTodoByIdAsync(1)).ReturnsAsync(todo);

            var result = await _controller.GetTodo(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(todo, okResult.Value);
        }

        [Test]
        public async Task PostTodo_ReturnsOkResult_WhenTodoIsCreated()
        {
            var todo = new Todo { Name = "Todo 1" };
            _mockService.Setup(s => s.CreateTodoAsync(todo)).ReturnsAsync(todo);

            var result = await _controller.PostTodo(todo);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(todo, okResult.Value);
        }

        [Test]
        public async Task PutTodo_ReturnsOkResult_WhenTodoIsUpdated()
        {
            var todo = new Todo { Id = 1, Name = "Todo 1" };
            _mockService.Setup(s => s.UpdateTodoAsync(1, todo)).ReturnsAsync(true);

            var result = await _controller.PutTodo(1, todo);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteTodo_ReturnsOkResult_WhenTodoIsDeleted()
        {
            _mockService.Setup(s => s.DeleteTodoAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteTodo(1);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}