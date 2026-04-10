using Backend_ЛР9_.Api.Student.Controllers;
using Backend_ЛР9_.Api.Student.Repositories;

namespace Backend_ЛР9_.Api.Student.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public List<StudentResponseContract> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(StudentUpsertRequestContract contract)
        {
            var students = _repository.GetAll();
            var nextId = students.Count == 0 ? 1 : students.Max(x => x.Id) + 1;

            var student = new StudentResponseContract
            {
                Id = nextId,
                Name = contract.Name,
                Surname = contract.Surname,
                Age = contract.Age,
                Group = contract.Group
            };

            _repository.Add(student);
        }
    }
}
