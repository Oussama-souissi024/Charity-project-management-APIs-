using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.Material_Mapper;
using CityProjects.Core.Transportation_Mapper;
using CityProjects.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TransportationController : ControllerBase
    {
        private readonly IDataHelper<Transportations> dataHelper;
        private readonly IMapper mapper;

        public TransportationController(IDataHelper<Transportations> dataHelper,
                                        IMapper mapper)
        {
            this.dataHelper = dataHelper;
            this.mapper = mapper;
        }

        // GET: api/<TransportationController>
        [HttpGet]
        [Authorize(Roles = "Admin,President, Secretary,ProjectManager")]
        public IEnumerable<GetTransportaionResponse> GetAllTransportations()
        {
            var transportations = dataHelper.GetAllData().ToList();
            var TransportaionList = mapper.Map<IEnumerable<GetTransportaionResponse>>(transportations);
            return TransportaionList;
        }

        // GET api/<TransportationController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,President, Secretary,ProjectManager")]
        public GetTransportaionResponse GetByID(int id)
        {
            var transportation = dataHelper.GetAllData().Where(t => t.TransportationId ==id ).FirstOrDefault();
            return mapper.Map<GetTransportaionResponse>(transportation); 
        }

        // POST api/<TransportationController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddTransportation([FromBody] CreateTransportationRequest Transportaion)
        {
            var NewTransportation = mapper.Map<Transportations>(Transportaion);
            dataHelper.Add(NewTransportation);
            return Ok(NewTransportation);
        }

        // PUT api/<TransportationController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public void Put(int id, [FromBody] CreateTransportationRequest Transportaion)
        {
            var UpdateTransportation = mapper.Map<Transportations>(Transportaion);
            dataHelper.Edit(id, UpdateTransportation);
        }

        // DELETE api/<TransportationController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            dataHelper.Delete(id);
        }
    }
}
