namespace NetCoreBackend.Domain.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

}
