namespace Users.Infrastructure.Domain
{
    public class UserCredentials
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
