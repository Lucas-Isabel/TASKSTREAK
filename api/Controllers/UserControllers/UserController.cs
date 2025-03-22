using api.ViewModel.UserViewModel;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Index()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users); 
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Details(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); 
            }
            return Ok(user); 
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            try
            {
                var user = new UserModel(model.Username, model.Email, model.Password);
                var result = await _userRepository.AddUserAsync(user);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(new { Message = "Usuário criado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                string errorResult = ErrorResponseFormatter.FormatErrorResponse(ex.Message);
                return BadRequest(errorResult);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] UserModel user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(); 
            }

            existingUser.Username = user.Username;
            existingUser.UpdateEmail(user.Email);
            await _userRepository.UpdateUserAsync(existingUser);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); 
            }

            await _userRepository.DeleteUserAsync(id);
            return NoContent(); 
        }
    }
}
