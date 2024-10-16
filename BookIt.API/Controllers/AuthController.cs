using Azure;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
using BookIt.API.Repositories;
using BookIt.API.Services;
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
        private readonly IEmailSender emailSender;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository,IEmailSender emailSender,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailSender = emailSender;
            this.signInManager = signInManager;
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
                Id = registerRequestDto.Id.ToString(),
                //TwoFactorEnabled = true
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
                    if (user.TwoFactorEnabled)
                    {
                        var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

                        await emailSender.SendEmailAsync(user.Email, "OTP Confrimation", token);

                        return Ok($"We have sent an OTP to your Email {user.Email}");
                    }

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

        [HttpPost]
        [Route("Login-2FA")]
        public async Task<IActionResult> LoginWithOTP(string code, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.VerifyTwoFactorTokenAsync(user, "Email", code);
                if (result)
                {
                    // Create Token
                    var jwtToken = tokenRepository.createJWTToken(user);

                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken
                    };

                    return Ok(response);
                }
            }
            return BadRequest("OTP incorrect");
        }

        [HttpPost]
        [Route("Email")]
        public async Task<IActionResult> Email(string email, string subject, string message)
        {
            try
            {
                await emailSender.SendEmailAsync(email, subject, message);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine(ex.Message);  // Use a logging library here
                return StatusCode(StatusCodes.Status500InternalServerError, "Email sending failed.");
            }
        }
    }
}
