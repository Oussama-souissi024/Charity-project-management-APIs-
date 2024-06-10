using AutoMapper;
using CityProjects.Core;
using CityProjects.Core.Material_Mapper;
using CityProjects.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class MaterialController : ControllerBase
    {
        private readonly IDataHelper<Materials> dataHelper;
        private readonly IMapper mapper;

        public MaterialController(IDataHelper<Materials> dataHelper,
                                  IMapper mapper)
        {
            this.dataHelper = dataHelper;
            this.mapper = mapper;
        }

        // GET: api/<MaterialController>
        [HttpGet]
        [Authorize(Roles = "Admin,President, Secretary,ProjectManager")]
        public IEnumerable<GetMaterialResponse> GetAllMaterial()
        {
            var materials = dataHelper.GetAllData().ToList();
            var MaterialList = mapper.Map<IEnumerable<GetMaterialResponse>>(materials);
            return MaterialList;
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,President, Secretary,ProjectManager")]
        public GetMaterialResponse GetMaterialByID(int id)
        {
            var Material = dataHelper.GetAllData().Where(m => m.MaterialId == id).FirstOrDefault();
            return mapper.Map<GetMaterialResponse>(Material);
        }  

        // POST api/<MaterialController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddMaterial([FromBody] CreateMaterialRequest Material)
        {
            var NewMaterial = mapper.Map<Materials>(Material);
            dataHelper.Add(NewMaterial);
            return Ok(NewMaterial);
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public void Put(int id, [FromBody]  CreateMaterialRequest Material)
        {
            var UpdateMaterial = mapper.Map<Materials>(Material);
            dataHelper.Edit(id, UpdateMaterial);
        }


        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            dataHelper.Delete(id);
        }
    }
}

