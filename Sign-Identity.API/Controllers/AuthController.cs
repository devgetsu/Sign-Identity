using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sign_Identity.Domain.DTOs;
using Sign_Identity.Domain.Entities.Auth;

namespace Sign_Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //check model
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong!");
            }
            // cehck dto
            if (string.IsNullOrWhiteSpace(registerDTO.Email))
            {
                return BadRequest("Email required");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Username))
            {
                return BadRequest("Username required");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.FirstName))
            {
                return BadRequest("FirstName required");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.LastName))
            {
                return BadRequest("LastName required");
            }

            //Mapping
            var user = new User()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Username,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Age = registerDTO.Age
            };

            //create user
            var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!createdUser.Succeeded)
            {
                return BadRequest("Something went wrong in Create");
            }
            return Ok("Congratz");
        }
    }
}
