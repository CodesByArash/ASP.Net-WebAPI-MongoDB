using AuthService.Entities;
using AuthService.Mappers;
using AuthService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository){
            _repository = repository;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users.Select(u => u.ToUserDto()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return NotFound("User not found");
            return Ok(user.ToUserDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            await _repository.UpdateAsync(id, user);
            return Ok(user.ToUserDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return Ok(new {Success = true, Message = "User Deleted"});
        }
    }
}