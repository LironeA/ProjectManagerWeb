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
    public class UsersAPIControllerTests
    {
        private Mock<IUserService> _mockService;
        private UsersAPIController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UsersAPIController(_mockService.Object);
        }

        [Test]
        public async Task GetUser_ReturnsOkResult_WithListOfUsers()
        {
            var users = new List<User> { new User { Id = 1, Name = "User 1" } };
            _mockService.Setup(s => s.GetUsersAsync()).ReturnsAsync(users);

            var result = await _controller.GetUser();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(users, okResult.Value);
        }

        [Test]
        public async Task GetUser_ReturnsOkResult_WithUser()
        {
            var user = new User { Id = 1, Name = "User 1" };
            _mockService.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetUser(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task PostUser_ReturnsOkResult_WhenUserIsCreated()
        {
            var user = new User { Name = "User 1" };
            _mockService.Setup(s => s.CreateUserAsync(user)).ReturnsAsync(user);

            var result = await _controller.PostUser(user);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task PutUser_ReturnsOkResult_WhenUserIsUpdated()
        {
            var user = new User { Id = 1, Name = "User 1" };
            _mockService.Setup(s => s.UpdateUserAsync(1, user)).ReturnsAsync(true);

            var result = await _controller.PutUser(1, user);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteUser_ReturnsOkResult_WhenUserIsDeleted()
        {
            _mockService.Setup(s => s.DeleteUserAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteUser(1);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}