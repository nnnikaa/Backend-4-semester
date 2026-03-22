using Backend_ЛР6_.Api.Student.Controllers;

namespace Backend_ЛР6_.Api.Student.Services
{
    public interface IStudentService
    {
        List<StudentResponseContract> GetAll();
        void Create(StudentUpsertRequestContract contract);
    }
}