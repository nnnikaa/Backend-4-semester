using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Controllers;

namespace Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Repositories
{
    public interface IStudentRepository
    {
        List<StudentResponseContract> GetAll();
        void Add(StudentResponseContract student);
    }
}