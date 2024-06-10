using CityProjects.Core;
using CityProjects.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IDataHelper<Projects> dataHelperProject;
        private readonly IDataHelper<Users> dataHelperUser;

        public StatisticController(IDataHelper<Projects> dataHelperProject,
                                   IDataHelper<Users> dataHelperUser)
        {
            this.dataHelperProject = dataHelperProject;
            this.dataHelperUser = dataHelperUser;
        }

        // GET: api/Statistic/AllProjects/5
        [HttpGet("AllProjects/")]
        public ActionResult<int> GetNumberOfAllProjectByRegionID()
        {
            try
            {
                int NumberOfAllProject = dataHelperProject.GetAllData().Count();
                return Ok(NumberOfAllProject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/Statistic/WaitingForConfirmation/5
        [HttpGet("WaitingForConfirmation/")]
        public ActionResult<int> GetWaitingForConfirmationStatusByRegionID()
        {
            try
            {
                int NumberOfUnconfirmedProject = dataHelperProject.GetAllData()
                    .Where(p => p.Status.Contains("attente de confir"))
                    .Count();
                return Ok(NumberOfUnconfirmedProject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/Statistic/Confirmed/5
        [HttpGet("Confirmed/")]
        public ActionResult<int> GetConfirmedProjectStatusByRegionID()
        {
            try
            {
                int NumberOfconfirmedProject = dataHelperProject.GetAllData()
                    .Where(p => p.Status.Contains("Projet en cour"))
                    .Count();
                return Ok(NumberOfconfirmedProject);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/Statistic/Canceled/5
        [HttpGet("Canceled/")]
        public ActionResult<int> GetCanceledProjectStatusByRegionID()
        {
            try
            {
                int NumberOfCanceledProjects = dataHelperProject.GetAllData()
                    .Where(p => p.Status.Contains("Annulé (ce projet a été supprimé)"))
                    .Count();
                return Ok(NumberOfCanceledProjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/Statistic/Completed/5
        [HttpGet("Completed/")]
        public ActionResult<int> GetCompletedProjectStatusByRegionID()
        {
            try
            {
                int NumberOfCompletedProjects = dataHelperProject.GetAllData()
                   .Where(p => p.Status.Contains("Projet est terminé"))
                   .Count();
                return Ok(NumberOfCompletedProjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int RegioIDByProjectManagerID(int ProjectManagerID)
        {
            int RegionD = dataHelperUser.GetAllData()
                         .Where(u => u.UserId == ProjectManagerID)
                         .Select(u => u.RegionId)
                         .FirstOrDefault();
            return RegionD;
        }
    }
}
