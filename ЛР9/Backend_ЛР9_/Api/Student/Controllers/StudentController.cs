using Backend_ЛР9_.Api.Student.Repositories;
using Backend_.Api.Student.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace Backend_ЛР6_.Api.Student.Controllers
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
