using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Controllers;

namespace Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Repositories
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
