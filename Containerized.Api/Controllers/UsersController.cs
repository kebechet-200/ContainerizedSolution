﻿using Containerized.Api.Persistance;
using Containerized.Api.Persistance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Containerized.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context is null)
                throw new ArgumentNullException(nameof(_context));

            if (_context.Users is null)
                throw new ArgumentNullException(nameof(_context.Users));

            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (_context is null)
                throw new ArgumentNullException(nameof(_context));

            if (_context.Users is null)
                throw new ArgumentNullException(nameof(_context.Users));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
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

            return NoContent();
        }

        private bool UserExists(int id)
        {
            if (_context is null)
                throw new ArgumentNullException(nameof(_context));

            if (_context.Users is null)
                throw new ArgumentNullException(nameof(_context.Users));

            return _context.Users.Any(e => e.Id == id);
        }
    }
}
