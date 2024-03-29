﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Domain.Entities;
using QuizApi.MapProfiles;
using QuizApi.Models;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [Route("update-user-profile")]
        [HttpPatch]
        [ProducesResponseType(typeof(ResponseModel), 404)]
        [ProducesResponseType(typeof(IdentityError), 400)]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> UpdateUserInfo([FromForm] UserProfileUpdateModel model)
        {
            var user = await GetCurrentUser();

            if (user == null)
            {
                return NotFound(new ResponseModel() { Status = "Error", Message = "User not found!" });
            }

            if (model.FullName != null) user.FullName = model.FullName;
            if (model.PhoneNumber != null) user.PhoneNumber = model.PhoneNumber;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new ResponseModel() { Status = "Success", Message = "Profile updated successfully!" });
            else
                return BadRequest(result.Errors);


        }

        [Authorize]
        [Route("my-profile")]
        [HttpGet]
        [ProducesResponseType(typeof(UserProfileModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 404)]

        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var userFromDb = await GetCurrentUser();
            if (userFromDb != null)
            {
                var userModel = Mapper.Map(userFromDb);
                return Ok(userModel);
            }
            else
            {
                return NotFound(new ResponseModel() { Status = "Error", Message = "User not found!" });
            }

        }

        [Authorize("Admin")]
        [Route("all-users")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserProfileModel>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var models = new List<UserProfileModel>();
            foreach (var user in users)
            {
                models.Add(Mapper.Map(user));
            }
            return Ok(models);

        }

        [Authorize("Admin")]
        [HttpDelete]
        [Route("{username}/delete")]

        public async Task<IActionResult> RemoveUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Ok(new ResponseModel { Status = "Success", Message = "User removed successfully" });
                }
                else
                {
                    // Handle errors if the user deletion fails
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Failed to remove user" });
                }
            }
            else
            {
                // User with the specified username not found
                return NotFound(new ResponseModel { Status = "Error", Message = "User not found" });
            }
        }

        [NonAction]
        public async Task<AppUser> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return user;
        }
    }
}
