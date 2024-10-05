using BookIt.API.Models.DTO;
using BookIt.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                PhoneNumber = registerRequestDto.PhoneNumber,
                Id = registerRequestDto.Id.ToString()
            };

            var identityResult=await userManager.CreateAsync(identityUser,registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                await userManager.AddClaimAsync(identityUser, new Claim(ClaimTypes.NameIdentifier, identityUser.Id));
                //await userManager.AddClaimAsync(identityUser, new Claim(ClaimTypes.Email, identityUser.Email));
                //return Ok("User was registered! Please login.");
                var response = new RegisterResponseDto
                {
                    message = "User was registered! Please login."
                };
                return Ok(response);
            }

            return BadRequest("Something went wrong");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user=await userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Create Token
                    var jwtToken=tokenRepository.createJWTToken(user);

                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken
                    };

                    return Ok(response);
                }
            }
            return BadRequest("Email or password incorrect");
        }
    }
}
