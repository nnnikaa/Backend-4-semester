using Backend_ЛР4_Воробьева_В.Д._241_333.Api.Student.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace Backend_ЛР4_Воробьева_В.Д._241_333.Api.Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<(int Id, string Name, string Surname, int Age, int Group)> _student = new();

        [HttpPost]
        public ActionResult Create([FromBody] StudentUpsertRequestContract contract)
        {
            var nextId = _student.Count == 0 ? 1 : _student.Max(x => x.Id) + 1;
            var student = (Id:nextId, Name : contract.Name, Surname: contract.Surname, Age: contract.Age, Group:contract.Group);
            _student.Add(student);

            return CreatedAtAction(
                nameof(StudentGen),
                new { id = student.Id },
                student
            );
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<StudentResponseContract[]> GetAllStudents()
        {
            var students = _student
                .Select(x => new StudentResponseContract { Id = x.Id, Name = x.Name, Surname = x.Surname, Age = x.Age, Group = x.Group })
                .ToArray();
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public ActionResult StudentGen(int id)
        {
            var index = _student.FindIndex(x => x.Id == id);
            if (index < 0) return NotFound();

            var student = _student[index];
            return Ok(new StudentResponseContract {Id = student.Id, Name = student.Name, Surname = student.Surname, Age = student.Age, Group = student.Group });
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public ActionResult Update(int id, [FromBody] StudentUpsertRequestContract contract)
        {
            var index = _student.FindIndex(x => x.Id == id);
            if (index < 0) return NotFound();

            var updated = (id, contract.Name, contract.Surname, contract.Age, contract.Group);
            _student[index] = updated;
            return Ok();

        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var index = _student.FindIndex(x => x.Id == id);
            if (index < 0) return NotFound();
            _student.RemoveAt(index);
            return NoContent();
        }

    }



}
