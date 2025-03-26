namespace OuathAuthentication.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = default!;
        public string LastName { get;set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int TimesOfLogin { get; set; }
        public DateTime LastLoginAt { get; set; }

    }
}
