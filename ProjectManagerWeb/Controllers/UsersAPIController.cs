﻿using System;
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
    public class UsersAPIController : ControllerBase
    {
        private readonly MyProjectManagerDBContext _context;

        public UsersAPIController(MyProjectManagerDBContext context)
        {
            _context = context;
        }

        // GET: api/UsersAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            _context.Projects.Load();
            return await _context.User.ToListAsync();
        }

        // GET: api/UsersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/UsersAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/UsersAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'MyProjectManagerDBContext.User'  is null.");
            }

            if(String.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Name is required.");
            }
           
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/UsersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
