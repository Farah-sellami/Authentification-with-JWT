using JobExpressBack.Models.DTOs;
using JobExpressBack.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobExpressBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }


        //Pour enregistrer un nouvel utilisateur.

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authRepository.Register(model);

            if (result == "Registration successful!")
            {
                return Ok(new { Message = result });
            }

            return BadRequest(new { Message = result });
        }

        //Login
        //Pour connecter un utilisateur et obtenir un token JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authRepository.Login(model);

            if (result == "Login successful!")
            {
                return Ok(new { 
                    Message = result 
                });
            }
            
                return BadRequest(new { Message = result }); 
        }

        }

        }
}

