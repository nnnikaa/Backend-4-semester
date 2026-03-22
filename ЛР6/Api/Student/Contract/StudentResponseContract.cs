namespace Backend_ЛР6_.Api.Student.Controllers
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
