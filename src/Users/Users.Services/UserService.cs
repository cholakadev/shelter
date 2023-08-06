using FluentResult;
using Users.Core.Repositories;
using Users.Core.Requests;
using Users.Core.Services;
using Users.Infrastructure.Domain;
using Users.Core.Helpers;
using Users.Core.Models.Users;

namespace Users.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _usersRepository;

        public UserService(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result<bool>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var salt = string.Empty;

            return await Result
                .Create(request)
                .Map(x => PasswordHelper.HashPassword(request.Password, out salt))
                .Map(x => request.ToEntity(x, salt))
                .MapAsync(user => _usersRepository.AddAsync(user))
                .ValidateAsync(user => user != null, ResultComplete.OperationFailed, "Failed to register user.")
                .MapAsync(user => user != null);
        }
    }
}
