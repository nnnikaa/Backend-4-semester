using Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Controllers;

namespace Backend_ЛР6_Воробьева_В.Д._241_333.Api.Student.Services
{
    public interface IStudentService
    {
        List<StudentResponseContract> GetAll();
        void Create(StudentUpsertRequestContract contract);
    }
}