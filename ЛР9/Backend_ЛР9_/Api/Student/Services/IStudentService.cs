using Backend_ЛР9_.Api.Student.Controllers;

namespace Backend_ЛР9_.Api.Student.Services
{
    public interface IStudentService
    {
        List<StudentResponseContract> GetAll();
        void Create(StudentUpsertRequestContract contract);
    }
}