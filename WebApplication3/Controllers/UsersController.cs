using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Classes;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly Models.ShopContext _context;

        public UsersController(Models.ShopContext context) {
            _context = context;
            _context.Database.EnsureCreated();

        }


        [HttpGet]
        public async Task<IActionResult> getAllUsers([FromQuery] QueryParameters queryParameters) {

            IQueryable<Models.Users> Users = _context.Users;
            Users = Users
                .Skip(queryParameters.size * (queryParameters.page - 1))
                .Take(queryParameters.size);
            return Ok(await Users.ToArrayAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<Models.Users>> PostUsers([FromBody] Models.Users Users)
        {
            var userWithSameEmail = _context.Users.Where(m => m.Email == Users.Email).SingleOrDefault();
            if (userWithSameEmail == null)
            {
                _context.Users.Add(Users);
                await _context.SaveChangesAsync();

                return CreatedAtAction("getUser", new { Users.Id }, Users);
            }else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] Models.Users Users)
        {
            if (id != Users.Id) {
                return BadRequest();
            }

            _context.Entry(Users).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Users.Find(id) == null) {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Users>> DeleteUser([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
;        }
    }
}
