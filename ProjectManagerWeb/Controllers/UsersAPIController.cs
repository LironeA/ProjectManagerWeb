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
    public class UsersAPIController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersAPIController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/UsersAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _userService.GetUsersAsync();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET: api/UsersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/UsersAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var updated = await _userService.UpdateUserAsync(id, user);
            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/UsersAPI
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            if (createdUser == null)
            {
                return BadRequest("Name is required.");
            }

            return Ok(createdUser);
        }

        // DELETE: api/UsersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
