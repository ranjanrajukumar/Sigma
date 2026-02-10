using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Interfaces;
using Sigma.Domain.Entities.Utilities;

namespace Sigma.API.Controllers.Utilities
{
    [ApiController]
    [Route("api/auth/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            // 🔐 HASH PASSWORD BEFORE SAVING
            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
            var id = await _repository.CreateAsync(user);
            return Ok(new { UserId = id });
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, User user)
        {
            user.UserId = id;
          
            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
            var updated = await _repository.UpdateAsync(user);

            return updated
                ? Ok("User updated successfully")
                : BadRequest("Update failed");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _repository.DeleteAsync(id);

            return deleted
                ? Ok("User deleted successfully")
                : BadRequest("Delete failed");
        }
    }
}
