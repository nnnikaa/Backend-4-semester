using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Repositories;
using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        public ActionResult<StudentResponseContract[]> GetAllStudents()
        {
            var students = _service.GetAll();
            return Ok(students);
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] StudentUpsertRequestContract contract)
        {
            _service.Create(contract);
            return Ok();
        }
    }
}
