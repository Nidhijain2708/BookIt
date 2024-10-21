using AutoMapper;
using BookIt.API.Data;
using BookIt.API.Models.Domain;
using BookIt.API.Models.DTO;
using BookIt.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly BookItDbContext dbContext;

        public CurrentUserController(ICurrentUserRepository currentUserRepository, IMapper mapper, UserManager<IdentityUser> userManager,BookItDbContext dbContext)
        {
            this.currentUserRepository = currentUserRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.dbContext = dbContext;
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
            var dbUser=await dbContext.Users.FirstOrDefaultAsync(u=>((u.user_id).ToString())==userId);

            // Check if the user exists
            if (identityUser == null)
            {
                return NotFound("User not found.");
            }

            // Map the identity user to your domain user model if needed
            var userDto = new UserDto()
            {
                email = identityUser.Email,
                //password = identityUser.Password,  // Be cautious when exposing passwords!
                phone_number = identityUser.PhoneNumber,
                first_name = identityUser.UserName,
                last_name=dbUser.last_name,
                preferred_currency= dbUser.preferred_currency,
                preferred_language= dbUser.preferred_language,
                profile_pic_path= dbUser.profile_pic_path
            };

            // Map the domain user to a DTO
            //var userDto = mapper.Map<UserDto>(userDomain);

            userDto.user_id = userId;

            // Return the DTO
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateUserRequestDto updateUserRequestDto)
        {
            // Retrieve the user ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId==null) User.FindFirst(ClaimTypes.NameIdentifier);
            var dbUser= await dbContext.Users.FirstOrDefaultAsync(u=>(u.user_id.ToString())==userId);
            Console.WriteLine(userId);

            // Ensure that the user ID is not null or empty
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is missing from claims.");
            }

            // Retrieve the user
            var user = await currentUserRepository.GetByIdAsync(userId);

            if (!string.IsNullOrEmpty(updateUserRequestDto.first_name))
            {
                user.UserName= updateUserRequestDto.first_name;
                dbUser.first_name = updateUserRequestDto.first_name;
            }
            
            if (!string.IsNullOrEmpty(updateUserRequestDto.last_name))
            {
                user.UserName= updateUserRequestDto.last_name;
                dbUser.first_name = updateUserRequestDto.last_name;
            }

            if (!string.IsNullOrEmpty(updateUserRequestDto.email))
            {
                if(new EmailAddressAttribute().IsValid(updateUserRequestDto.email))
                {
                    user.Email = updateUserRequestDto.email;
                    dbUser.email= updateUserRequestDto.email;
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
                    dbUser.phone_number= updateUserRequestDto.phone_number;
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
                        dbUser.password= updateUserRequestDto.newPassword;
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

            if (!string.IsNullOrEmpty(updateUserRequestDto.preferred_language))
            {
                dbUser.preferred_language= updateUserRequestDto.preferred_language;
            }

            if (!string.IsNullOrEmpty(updateUserRequestDto.preferred_currency))
            {
                dbUser.preferred_currency = updateUserRequestDto.preferred_currency;
            }

            if (updateUserRequestDto.profilePicture != null && !string.IsNullOrEmpty(updateUserRequestDto.filename))
            {
                var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                // Generate a unique file name for the uploaded image
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(updateUserRequestDto.filename.Replace(" ", "_"))}";
                var localFilePath = Path.Combine(imagesFolder, uniqueFileName);

                // Save the new image to the images folder
                using (var stream = new FileStream(localFilePath, FileMode.Create))
                {
                    await updateUserRequestDto.profilePicture.CopyToAsync(stream);
                }

                // Update the user's profile picture path
                dbUser.profile_pic_path = $"/Images/{uniqueFileName}";
            }

            dbContext.Users.Update(dbUser);
            await dbContext.SaveChangesAsync();

            var result =await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Some error occurred while updating.");
            }

            //var response = new UpdateUserResponseDto()
            //{
            //    message = "User updated success."
            //};

            //return Ok(response);

            return Ok(dbUser);
        }
    }
}
