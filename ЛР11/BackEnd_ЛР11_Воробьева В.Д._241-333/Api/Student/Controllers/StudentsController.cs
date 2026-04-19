using BackEnd_ЛР11_Воробьева_В.Д._241_333.Api.Student.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace BackEnd_ЛР11_Воробьева_В.Д._241_333.Api.Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(ILogger<StudentsController> logger) : ControllerBase
    {
        private static List<(int Id, string Name, string Surname, int Age, int Group)> _student = new();

        [HttpPost]
        public ActionResult Create([FromBody] StudentUpsertRequestContract contract)
        {
            logger.LogInformation("Получен запрос на создание студента. Имя: {Name}, Фамилия: {Surname},  Возраст: {Age}, Группа: {Group}", contract.Name, contract.Surname, contract.Age, contract.Group);
            var nextId = _student.Count == 0 ? 1 : _student.Max(x => x.Id) + 1;
            var student = (Id:nextId, Name : contract.Name, Surname: contract.Surname, Age: contract.Age, Group:contract.Group);
            _student.Add(student);

            logger.LogInformation("Студент создан успешно. Общее число студентов: {Count}", _student.Count);


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
            logger.LogInformation("Получить запрос на получение всех студентов. Общее число студентов: {Count}", _student.Count);

            var students = _student
                .Select(x => new StudentResponseContract { Id = x.Id, Name = x.Name, Surname = x.Surname, Age = x.Age, Group = x.Group })
                .ToArray();
            return Ok(students);

        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public ActionResult StudentGen(int id)
        {
            logger.LogInformation("Получить студента по полученному запросу на идентификатор. Id: {Id}", id);


            var index = _student.FindIndex(x => x.Id == id);
            if (index < 0) return NotFound();

            var student = _student[index];
            logger.LogInformation(" Студент найден. Id: {Id}, Имя: {Name}, Фамилия: {Surname}", student.Id, student.Name, student.Surname);
            return Ok(new StudentResponseContract {Id = student.Id, Name = student.Name, Surname = student.Surname, Age = student.Age, Group = student.Group });
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public ActionResult Update(int id, [FromBody] StudentUpsertRequestContract contract)
        {
            logger.LogInformation("Получен запрос на обновление студента. Id: {Id}, Новое имя: {Name}, Новая фамилия: {Surname}, Новый возраст: {Age}, Новая группа: {Group}", id, contract.Name, contract.Surname, contract.Age, contract.Group);
            var index = _student.FindIndex(x => x.Id == id);
            if (index < 0)
            {
                logger.LogWarning("Не найден студент для обнавления информации. Id: {Id}", id);
                return NotFound();
            }

            var updated = (id, contract.Name, contract.Surname, contract.Age, contract.Group);
            _student[index] = updated;

            logger.LogInformation("Студент успешно обнавлен. Id: {Id}", id);
            return Ok();

        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            logger.LogInformation("Получен запрос на удаление студента. Id: {Id}", id);

            var index = _student.FindIndex(x => x.Id == id);
            if (index < 0)
            {
                logger.LogWarning("Не найден студент для удаления. Id: {Id}", id);
                return NotFound();
            }
            _student.RemoveAt(index);

            logger.LogInformation("Студент успешно удален. Id: {Id}, Новое число студентов: {Count}", id, _student.Count);

            return NoContent();
        }

    }



}
