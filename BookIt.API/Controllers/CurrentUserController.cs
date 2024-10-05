using AutoMapper;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
using BookIt.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BookIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentUserController : ControllerBase
    {
        private readonly ICurrentUserRepository currentUserRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public CurrentUserController(ICurrentUserRepository currentUserRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.currentUserRepository = currentUserRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            // Retrieve the user ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ensure that the user ID is not null or empty
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is missing from claims.");
            }

            // Retrieve the user from the repository
            var identityUser = await currentUserRepository.GetByIdAsync(userId);

            // Check if the user exists
            if (identityUser == null)
            {
                return NotFound("User not found.");
            }

            // Map the identity user to your domain user model if needed
            var userDomain = new User()
            {
                email = identityUser.Email,
                //password = identityUser.Password,  // Be cautious when exposing passwords!
                phone_number = identityUser.PhoneNumber,
                name = identityUser.UserName,
            };

            // Map the domain user to a DTO
            var userDto = mapper.Map<UserDto>(userDomain);

            userDto.user_id = userId;

            // Return the DTO
            return Ok(userDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            // Retrieve the user ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ensure that the user ID is not null or empty
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is missing from claims.");
            }

            // Retrieve the user
            var user = await currentUserRepository.GetByIdAsync(userId);

            if (!string.IsNullOrEmpty(updateUserRequestDto.name))
            {
                user.UserName= updateUserRequestDto.name;
            }

            if (!string.IsNullOrEmpty(updateUserRequestDto.email))
            {
                if(new EmailAddressAttribute().IsValid(updateUserRequestDto.email))
                {
                    user.Email = updateUserRequestDto.email;
                }
                else
                {
                    var res = new UpdateUserResponseDto()
                    {
                        message = "Email not valid."
                    };

                    return BadRequest(res);
                }
            }

            if (!string.IsNullOrEmpty(updateUserRequestDto.phone_number))
            {
                if (updateUserRequestDto.phone_number.Length == 10)
                {
                    user.PhoneNumber = updateUserRequestDto.phone_number;
                }
                else
                {
                    var res = new UpdateUserResponseDto()
                    {
                        message = "Phone number should be 10 digits long."
                    };

                    return BadRequest(res);
                }
                
            }

            if (!string.IsNullOrEmpty(updateUserRequestDto.newPassword))
            {
                if(updateUserRequestDto.newPassword.Length >= 6)
                {
                    var isPasswordValid = await userManager.CheckPasswordAsync(user, updateUserRequestDto.currentPassword);
                    if (!isPasswordValid)
                    {
                        var res = new UpdateUserResponseDto()
                        {
                            message = "Current password is incorrect."
                        };

                        return BadRequest(res);
                    }
                    else
                    {
                        await userManager.ChangePasswordAsync(user, updateUserRequestDto.currentPassword, updateUserRequestDto.newPassword);
                    }
                }
                else
                {
                    var res = new UpdateUserResponseDto()
                    {
                        message = "New Password length less than 6."
                    };

                    return BadRequest(res);
                }
            }

            var result =await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Some error occurred while updating.");
            }

            var response = new UpdateUserResponseDto()
            {
                message = "User updated success."
            };

            return Ok(response);
        }
    }
}
