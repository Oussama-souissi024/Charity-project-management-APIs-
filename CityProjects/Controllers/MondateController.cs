using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.Mapper.Mondate_Mapper;
using CityProjects.Core.Material_Mapper;
using CityProjects.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MondateController : ControllerBase
    {
        private readonly IDataHelper<Mandates> dataHelperMandates;
        private readonly IDataHelper<Users> dataHelperUsers;
        private readonly IMapper mapper;

        public MondateController(IDataHelper<Mandates> dataHelperMandates,
                                 IDataHelper<Users> dataHelperUsers,
                                 IMapper mapper)
        {
            this.dataHelperMandates = dataHelperMandates;
            this.dataHelperUsers = dataHelperUsers;
            this.mapper = mapper;
        }
        // GET: api/<MondateController>
        [HttpGet]
        public IEnumerable<GetMondateResponse> Get()
        {
            var mandates = dataHelperMandates.GetAllData().ToList();
            var mandateList = mapper.Map<IEnumerable<GetMondateResponse>>(mandates);
            return mandateList;
        }

        // GET api/<MondateController>/5
        [HttpGet("{id}")]
        public GetMondateResponse Get(int id)
        {
            var mandate = dataHelperMandates.GetAllData().Where(m => m.MandateId == id).FirstOrDefault();
            return mapper.Map<GetMondateResponse>(mandate);

        }
    }
}
