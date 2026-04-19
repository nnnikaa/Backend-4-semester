namespace BackEnd_ЛР11_Воробьева_В.Д._241_333.Api.Student.Contract
{
    public class StudentResponseContract
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Surname { get; init; }
        public required int Age { get; init; }
        public required int Group { get; init; }
    }
}
