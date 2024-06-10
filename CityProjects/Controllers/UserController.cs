using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.User_Mapper;
using CityProjects.Data;
using CityProjects.Data.SqlServerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataHelper<Users> dataHelper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper _mapper;
        public string CurrentUserId;

        public UserController(
            IDataHelper<Users> dataHelper,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            this.dataHelper = dataHelper;
            this.userManager = userManager;
            this._mapper = mapper;
        }

        private async Task SetUserID(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            CurrentUserId = user?.Id;
        }



        // POST api/<UserController>
        [HttpPost("{Mail}")]
        [Authorize]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequest user , string Mail)
        {
            if (user.CityUserRoleId == 3 || user.CityUserRoleId == 4)
            {
                // Get the ID of the currently logged in user
                SetUserID(Mail);

                var newUser = _mapper.Map<Users>(user);
                
                newUser.AuthenticationUserId = CurrentUserId;
                newUser.IsActive = false;

                //Add The New User
                dataHelper.Add(newUser);

                // Add The New Role To Table AspNetRoles
                var currentUserObject = await userManager.FindByIdAsync(CurrentUserId);
                if (currentUserObject != null)
                {
                    if(user.CityUserRoleId  == 3)
                        await userManager.AddToRoleAsync(currentUserObject, "ProjectManager");
                    else if (user.CityUserRoleId == 4)
                        await userManager.AddToRoleAsync(currentUserObject, "Member");
                }
                else
                {
                    // Gérer le cas où l'utilisateur actuel n'est pas trouvé
                    return NotFound("Current user not found");
                }

                return Ok(newUser);
            }
            else
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
        }


        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<GetUserResponse> GetallUsers()
        {
            //var users = dataHelper.GetAllData().ToList();
            var users = dataHelper.GetAllData().AsQueryable()
                .Include(u => u.CityUserRole)
                .Include(u => u.Region)
                .ToList();
            var UsersListMapper = _mapper.Map<IEnumerable<GetUserResponse>>(users);
            return UsersListMapper;
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public GetUserResponse Get(int id)
        {
            var user = dataHelper.GetAllData().Where(u => u.UserId == id).AsQueryable()
                .Include(u => u.CityUserRole)
                .Include(u => u.Region)
                .FirstOrDefault();
            return _mapper.Map<GetUserResponse>(user);
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public void Put(int id, [FromBody] CreateUserRequest user)
        {
            var UpdatedUser = _mapper.Map<Users>(user);
            dataHelper.Edit(id, UpdatedUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            dataHelper.Delete(id);
        }
    }
}
