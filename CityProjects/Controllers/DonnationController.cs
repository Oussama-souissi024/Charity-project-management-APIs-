using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.Donation_Mapper;
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
   
    public class DonnationController : ControllerBase
    {
        private readonly IDataHelper<Donations> dataHelper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        public string CurrentUserId;

        public DonnationController(IDataHelper<Donations> dataHelper,
                                   UserManager<IdentityUser> userManager,
                                   IMapper mapper)
        {
            this.dataHelper = dataHelper;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        private async Task SetUserID(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            CurrentUserId = user?.Id;
        }


        // GET: api/<DonnationController>
        [HttpGet]
        [Authorize(Roles = "Admin,President, Secretary")]
        public IEnumerable<GetDonnationResponse> GetAllDonations()
        {
            var Donnations = dataHelper.GetAllData().AsQueryable()
                .Include(d => d.Member)
                .Include(d => d.Project)
                .ToList();
            var DonnationsListMapper = mapper.Map<IEnumerable<GetDonnationResponse>>(Donnations);
            return DonnationsListMapper;
        }

        // GET api/<DonnationController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,President, Secretary,Member")]
        public GetDonnationResponse Get(int id)
        {
            var Donnation = dataHelper.GetAllData().Where(d => d.DonationId == id).AsQueryable()
                .Include(d => d.Member)
                .Include(d => d.Project)
                .FirstOrDefault();
            return mapper.Map<GetDonnationResponse>(Donnation);
        }

        // POST api/<DonnationController>
        [HttpPost("{Mail}")]
        [Authorize(Roles = "Member")]
        public IActionResult AddDonnation([FromBody] CreateDonnationRequest Donnation, string Mail)
        {
            var NewDonnation = mapper.Map<Donations>(Donnation);
            SetUserID(Mail);
            dataHelper.Add2(NewDonnation, CurrentUserId);

            return Ok();
        }

        // PUT api/<DonnationController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Member")]
        public void Put(int id, [FromBody] UpdateDonnationRequest Donnation )
        {
            var LastDonnaition = dataHelper.Find(id);
            decimal LastAmount = LastDonnaition.Amount;

            var MemberID = LastDonnaition.MemberId;

            var DonnationUpdate = mapper.Map<Donations>(Donnation);
            DonnationUpdate.MemberId = MemberID; 

            var ProjectID = LastDonnaition.ProjectId;
            dataHelper.Edit2(id, ProjectID, DonnationUpdate, LastAmount);
        }

    }
}
