using Users.Core.Domain.Contracts;

namespace Users.Infrastructure.Domain
{
    public class User : IActivatable
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Type { get; set; }

        public bool Active { get ; set; }

        public UserCredentials Credentials { get; set; }
    }
}
