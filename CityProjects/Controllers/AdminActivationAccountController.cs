using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.Mapper.User_Mapper;
using CityProjects.Core.User_Mapper;
using CityProjects.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class AdminActivationAccountController : ControllerBase
    {

        private readonly IDataHelper<Users> dataHelper;
        private readonly IDataHelper<Users> dataHelperUser;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IDataHelper<Mandates> dataHelperMandates;
        private readonly IMapper mapper;
        public string CurrentUserId;
        public AdminActivationAccountController(
            IDataHelper<Users> dataHelperUser,
            UserManager<IdentityUser> userManager,
            IDataHelper<Mandates> dataHelperMandates,
            IMapper mapper)    
        {
            this.dataHelperUser = dataHelperUser;
            this.userManager = userManager;
            this.dataHelperMandates = dataHelperMandates;
            this.mapper = mapper;
        }


        // PUT api/<AdminActivationAccountController>/5
        [HttpPut("President/{id}")]
        public async Task<IActionResult> PutPresident(int id)
        {
            var UpdatedUser = dataHelperUser.Find(id);

            if (UpdatedUser == null) 
            {
                return NotFound("This ID is not found");
            }

            if (dataHelperUser.Edit2(id,1,UpdatedUser,1) == 0)
            {
                return NotFound("Current user not found");
            }

            var currentUserObject = await userManager.FindByIdAsync(UpdatedUser.AuthenticationUserId);

            if (currentUserObject != null)
            {
                var existingRoles = await userManager.GetRolesAsync(currentUserObject);
                await userManager.RemoveFromRolesAsync(currentUserObject, existingRoles.ToArray());
                await userManager.AddToRoleAsync(currentUserObject, "President");
                var NewMandate = new Mandates()
                {
                    StartDate = DateTime.Now,
                    EndtDate = DateTime.Now.AddMonths(3),
                    PresidentId = UpdatedUser.UserId,
                    IsActive = true
                };
                dataHelperMandates.Add(NewMandate);
                return Ok("Role updated successfully and Mandate created");
            }
            else
            {
                // Handle the case where the current user is not found
                return NotFound("Current user  not found");
            }
        }

        [HttpPut("Secretary/{id}")]
        public async Task<IActionResult> PutSecretary(int id)
        {
            var UpdatedUser = dataHelperUser.Find(id);

            if (UpdatedUser == null)
            {
                return NotFound("This ID is not found");
            }

            if (dataHelperUser.Edit2(id, 2, UpdatedUser, 2) == 0)
            {
                return NotFound("Current user not found");
            }

            var currentUserObject = await userManager.FindByIdAsync(UpdatedUser.AuthenticationUserId);

            if (currentUserObject != null)
            {
                var existingRoles = await userManager.GetRolesAsync(currentUserObject);
                await userManager.RemoveFromRolesAsync(currentUserObject, existingRoles.ToArray());
                await userManager.AddToRoleAsync(currentUserObject, "Secretary");
                return Ok("Role updated successfully");
            }
            else
            {
                // Handle the case where the current user is not found
                return NotFound("Current user  not found");
            }
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<string>> GetUnconfirmedEmailsAsync()
        {
            // Retrieve users whose Email Confirmed column is false
            var unconfirmedUsers = await userManager.Users.Where(u => u.EmailConfirmed == false).ToListAsync();

            // Extract the email addresses of these users
            var unconfirmedEmails = unconfirmedUsers.Select(u => u.Email).ToList();

            return unconfirmedEmails;
        }

        [HttpPut("ConfirmAccount")]
        public  async Task<bool> ConfirmUserByEmailAsync(string mail)
        {
            // Find user by email address
            var user = await userManager.FindByEmailAsync(mail);
            if (user == null)
                return false;

            // Update EmailConfirmed column to true
            user.EmailConfirmed = true;
            var result = await userManager.UpdateAsync(user);

           

            // Update IsActive column to true(Table Users)
            var UpdateTableUsers = dataHelperUser.Find3(user.Id);
            UpdateTableUsers.IsActive = true;

            string role = "";
            if (UpdateTableUsers.CityUserRoleId == 3)
                role = "ProjectManager";
            else if(UpdateTableUsers.CityUserRoleId == 4)
                role = "Member";

            await userManager.AddToRoleAsync(user,role);

            dataHelperUser.Edit(UpdateTableUsers.UserId, UpdateTableUsers);

            return true;
        }
    }
}
