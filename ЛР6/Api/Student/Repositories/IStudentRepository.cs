using Backend_ЛР6_.Api.Student.Controllers;

namespace Backend_ЛР6_.Api.Student.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private static List<StudentResponseContract> _students = new();

        public List<StudentResponseContract> GetAll()
        {
            return _students;
        }

        public void Add(StudentResponseContract student)
        {
            _students.Add(student);
        }
    

    }
}
