using LibrarySystem.Application.DTO;
using LibrarySystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            var inputUser = await _userService.AddUser(user);
            return Ok(inputUser);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserDTO user = await _userService.GetUserDetail(id);
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser([FromBody] UserDTO user, int id)
        {
            var updatedUser = await _userService.UpdateUser(user, id);
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok("User is deleted");
        }
        [HttpPatch("add_Note/{id}")]
        public async Task<IActionResult> AddNote([FromBody] string note,int id)
        {
            var updatedUser = await _userService.AddNote(note,id);
            return Ok(updatedUser);
        }
    }
}
