using Microsoft.AspNetCore.Mvc;
using Slot2API.DTOs;
using Slot2API.Services;

namespace Slot2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetUserDTO>> GetUser(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
                
        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> CreateUser(CreateUserDTO user)
        {
            var createdUser = await _userService.Create(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<GetUserDTO>> UpdateUser(int id, UpdateUserDTO user)
        {
            var updatedUser = await _userService.Update(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userService.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
