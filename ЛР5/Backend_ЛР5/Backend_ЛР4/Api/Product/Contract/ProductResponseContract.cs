namespace Backend_ЛР4_Воробьева_В.Д._241_333.Api.Product.Contract
{

    public class ProductResponseContract
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Category { get; init; }
        public required int Amount { get; init; }
        public required string Description { get; init; }
        public required DateTime ProductionDate { get; init; }

        public required Guid Guid { get; init; }
    }
}