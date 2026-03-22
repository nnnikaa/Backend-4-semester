using Backend_ЛР6_.Api.Student.Controllers;

namespace Backend_ЛР6_.Api.Student.Repositories
{
    public interface IStudentRepository
    {
        List<StudentResponseContract> GetAll();
        void Add(StudentResponseContract student);
    }
}