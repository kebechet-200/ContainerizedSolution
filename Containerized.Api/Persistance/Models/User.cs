namespace Containerized.Api.Persistance.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
