using System.ComponentModel.DataAnnotations;

namespace BackEnd_ЛР11_Воробьева_В.Д._241_333.Api.Student.Contract
{
    public class StudentUpsertRequestContract
    {

        [MaxLength(40)]
        [MinLength(2)]
        [RegularExpression(@"^([A-ZА-Я][a-zа-я]*[ ]*)+$")]
        public required string Name { get; init; }

        [MaxLength(40)]
        [MinLength(2)]
        [RegularExpression(@"^([A-ZА-Я][a-zа-я]*[ ]*)+$")]
        public required string Surname { get; init; }

        [Range(15, 110, ErrorMessage = "Возраст должен быть между 15 и 110")]

        public required int Age { get; init; }

        public required int Group { get; init; }
    }
}
