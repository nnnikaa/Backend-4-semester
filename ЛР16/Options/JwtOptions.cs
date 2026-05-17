namespace BackEnd_ЛР16_Воробьева_В.Д._241_333.Options
{
    public class JwtOptions
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; } 
        public required string Key { get; init; }
    }
}
