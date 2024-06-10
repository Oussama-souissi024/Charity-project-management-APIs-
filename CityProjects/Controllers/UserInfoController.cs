using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("userinfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = new
            {
                Id = user.Id,
                Roles = roles
            };

            return Ok(userInfo);
        }
    }

}
