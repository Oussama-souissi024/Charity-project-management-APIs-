using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.Mapper.Project_Mapper;
using CityProjects.Core.Project_Mapper;
using CityProjects.Data;
using CityProjects.Data.SqlServerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IDataHelper<Projects> dataHelper;
        private readonly IDataHelper<Users> dataHelperUser;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly DataContext dbContext;
        public string CurrentUserId;

        public ProjectController(IDataHelper<Projects> dataHelper,
                                 IDataHelper<Users>dataHelperUser,
                                 IMapper mapper,
                                 UserManager<IdentityUser> userManager,
                                 DataContext dbContext)
        {
            this.dataHelper = dataHelper;
            this.dataHelperUser = dataHelperUser;
            this.mapper = mapper;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        private async Task SetUserID(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            CurrentUserId = user?.Id;
        }


        // GET: api/<ProjectController>
        [HttpGet]
        [Authorize]
        public IEnumerable<GetProjectResponse> GetAllProjects()
        {
            var projects = dataHelper.GetAllData().AsQueryable()
                .Include(p => p.User)
                .Include(p => p.Material)
                .Include(p => p.Transportations)
                .ToList();
            var projectsListMapper = mapper.Map<IEnumerable<GetProjectResponse>>(projects);
            return projectsListMapper;
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,President, Secretary,ProjectManager")]
        public GetProjectResponse GetByID(int id)
        {
            var project = dataHelper.GetAllData().Where(p => p.ProjectId == id).AsQueryable()
                .Include(p => p.User)
                .Include(p => p.Material)
                .Include(p => p.Transportations)
                .FirstOrDefault();
            return mapper.Map<GetProjectResponse>(project);
        }

        // POST api/<ProjectController>
        [HttpPost("{Mail}")]
        [Authorize(Roles = "ProjectManager")]
        public IActionResult AddProject([FromBody] CreateProjectRequest projectRequest, string Mail)
        {
            var newProject = mapper.Map<Projects>(projectRequest);
            SetUserID(Mail);

            // Récupérer les entités Transportation basées sur les IDs fournis
            var transportations = dbContext.Transportations
                                           .Where(t => projectRequest.TransportationIds.Contains(t.TransportationId))
                                           .ToList();

            // Associer les transportations au projet
            newProject.Transportations = transportations;

            dataHelper.Add2(newProject, CurrentUserId);
            return Ok(newProject);
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ProjectManager")]
        public IActionResult Put(int id, [FromBody] CreateProjectRequest projectRequest)
        {
            var projectUpdate = mapper.Map<Projects>(projectRequest);

            // Récupérer les entités Transportation basées sur les IDs fournis
            var transportations = dbContext.Transportations
                                           .Where(t => projectRequest.TransportationIds.Contains(t.TransportationId))
                                           .ToList();

            // Associer les transportations au projet
            

            var FindProjectToUpdate = dataHelper.Find(id);

            FindProjectToUpdate.Name = projectUpdate.Name;
            FindProjectToUpdate.Budget = projectUpdate.Budget;
            FindProjectToUpdate.StartDate = projectUpdate.StartDate;
            FindProjectToUpdate.EndtDate = projectUpdate.EndtDate;
            FindProjectToUpdate.Location = projectUpdate.Location;
            FindProjectToUpdate.Description = projectUpdate.Description;
            FindProjectToUpdate.MaterialProjectID = FindProjectToUpdate.MaterialProjectID;
            FindProjectToUpdate.Transportations = transportations;

            dataHelper.Edit(id, FindProjectToUpdate);
            return  StatusCode(StatusCodes.Status200OK,"Update Successfully");
        }

        // PUT api/<ProjectController>/5
        [HttpPut("UpdateProjectApprovalScretary/{id}")]
        [Authorize(Roles = "Secretary")]
        public async Task<IActionResult> UpdateProjectApprovalScretary(int id)
        {
            
            var projectUpdate = dataHelper.Find(id);
            projectUpdate.SecretaryApproval = true;
            dataHelper.Edit(id, projectUpdate);
            return Ok(projectUpdate);
        }

        // PUT api/<ProjectController>/5
        [HttpPut("UpdateProjectApprovalPresident/{id}")]
        //[Authorize(Roles = "President")]
        public async Task<IActionResult> UpdateProjectApprovalPresident(int id)
        {

            var ProjectToUpdte = dataHelper.Find(id);
            if(ProjectToUpdte.SecretaryApproval == false)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "En attente de la confirmation du secrétaire tout d'abord");
            }
            ProjectToUpdte.PresidentApproval = true;
            ProjectToUpdte.Status = "Projet en cour";
            dataHelper.Edit(id, ProjectToUpdte);

       

            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT api/<ProjectController>/5
        [HttpPut("FinalizationOfTheProjectByChangingTheStatus/{id}")]
        //[Authorize(Roles = "ProjectManager")]
        public async Task<IActionResult> FinalizationOfTheProjectByChangingTheStatus(int IdProject, int IdProjectManager)
        {
            // Check if this project Manager exist in this project
            var ProjectToUpdte = dataHelper.Find(IdProject);
            if(ProjectToUpdte.ProjectManagerId != IdProjectManager)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "It is not the project manager who is responsible for this project");

            }

            ProjectToUpdte.Status = "Projet est terminé";
            dataHelper.Edit(IdProject, ProjectToUpdte);
            return Ok(ProjectToUpdte);
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,President, Secretary,ProjectManager")]
        public void Delete(int id)
        {
            dataHelper.Delete(id);
        }


        private int GetRigiondByProjectMangerId(int ProjectManagerId)
        {
            return dataHelperUser.GetAllData().Where(u => u.UserId == ProjectManagerId).Select(u => u.RegionId).FirstOrDefault();
        }

        private int GetProjectManagerIdByProjectId(int ProjectID)
        {
            return dataHelper.GetAllData().Where(p => p.ProjectId == ProjectID).Select(p => p.ProjectManagerId).FirstOrDefault();
        }

    }

}
