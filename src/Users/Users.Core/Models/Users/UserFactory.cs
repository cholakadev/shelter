using Users.Core.Requests;
using Users.Infrastructure.Domain;
#pragma warning disable CS8603 // Possible null reference return.

namespace Users.Core.Models.Users
{
    public static class UserFactory
    {
        /// <summary>Convert <see cref="RegisterRequest"/> to <see cref="User"/>.</summary>
        public static User ToEntity(this RegisterRequest request, string passwordHash, string passwordSalt)
        {
            return request == null
                ? null
                : new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Type = request.Type.ToString(),
                    Credentials = passwordHash.ToEntity(passwordSalt),
                };
        }

        /// <summary>Convert to <see cref="UserCredentials"/>.</summary>
        public static UserCredentials ToEntity(this string passwordHash, string passwordSalt)
        {
            return passwordHash == null || passwordSalt == null
                ? null
                : new UserCredentials
                {
                    Password = passwordHash,
                    Salt = passwordSalt,
                };
        }
    }
}
