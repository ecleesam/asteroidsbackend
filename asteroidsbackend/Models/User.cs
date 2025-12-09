namespace asteroidsbackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int Credits { get; set; } = 1000; // Starting credits
    }
}
