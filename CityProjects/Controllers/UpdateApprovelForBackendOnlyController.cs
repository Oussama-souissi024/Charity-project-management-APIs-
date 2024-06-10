using AutoMapper;
using CityProjects.Core;
using CityProjects.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateApprovelForBackendOnlyController : ControllerBase
    {
        private readonly IDataHelper<Projects> dataHelper;
        private readonly IDataHelper<Users> dataHelperUser;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        
        public UpdateApprovelForBackendOnlyController(IDataHelper<Projects> dataHelper,
                                 IDataHelper<Users> dataHelperUser,
                                 IMapper mapper,
                                 UserManager<IdentityUser> userManager)
        {
            this.dataHelper = dataHelper;
            this.dataHelperUser = dataHelperUser;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // PUT api/<UpdateApprovelForBackendOnlyController>
        [HttpPut("UpdateProjectApprovalScretary/{id}")]
        [Authorize(Roles = "Secretary")]
        public async Task<IActionResult> UpdateProjectApprovalScretary(int id)
        {

            SetUserID();

            var ProjectRegionId = GetRigiondByProjectMangerId(GetProjectManagerIdByProjectId(id));

            var CurrentScretaryRegionId = GetRegiodIDByAuthenticatedUserId(CurrentUserId);

            if (CurrentScretaryRegionId != ProjectRegionId)
            {
                return NotFound("this Scretary has no right to validate this project");
            }

            var projectUpdate = dataHelper.Find(id);
            projectUpdate.SecretaryApproval = true;
            dataHelper.Edit(id, projectUpdate);
            return Ok(projectUpdate);
        }

        // PUT api/<UpdateApprovelForBackendOnlyController>
        [HttpPut("UpdateProjectApprovalPresident/{id}")]
        //[Authorize(Roles = "President")]
        public async Task<IActionResult> UpdateProjectApprovalPresident(int id)
        {
            var CurrentUserId = await SetUserID();
            var CurrentPresidentRegionId = GetRegiodIDByAuthenticatedUserId(CurrentUserId);

            var ProjectRegionId = GetRigiondByProjectMangerId(GetProjectManagerIdByProjectId(id));

            if (CurrentPresidentRegionId != ProjectRegionId)
            {
                return NotFound("this president has no right to validate this project");
            }

            var ProjectToUpdte = dataHelper.Find(id);
            if (ProjectToUpdte.SecretaryApproval == false)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "En attente de la confirmation du secrétaire tout d'abord");
            }
            ProjectToUpdte.PresidentApproval = true;
            ProjectToUpdte.Status = "Projet en cour";
            dataHelper.Edit(id, ProjectToUpdte);
            return StatusCode(StatusCodes.Status200OK);
        }

        private async Task<string> SetUserID()
        {
            // Assume that "userManager" is already defined in your class
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                return user?.Id;
            }
            else
            {
                return null;
            }
        }



        private int GetRigiondByProjectMangerId(int ProjectManagerId)
        {
            return dataHelperUser.GetAllData().Where(u => u.UserId == ProjectManagerId).Select(u => u.RegionId).FirstOrDefault();
        }

        private int GetProjectManagerIdByProjectId(int ProjectID)
        {
            return dataHelper.GetAllData().Where(p => p.ProjectId == ProjectID).Select(p => p.ProjectManagerId).FirstOrDefault();
        }

        private int GetRegiodIDByAuthenticatedUserId(string? AuthenticatedUserId)
        {
            return dataHelperUser.GetAllData().Where(u => u.AuthenticationUserId == AuthenticatedUserId).Select(u => u.RegionId ).FirstOrDefault();
        }
    }
}
