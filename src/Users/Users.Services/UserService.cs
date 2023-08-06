using FluentResult;
using Users.Core.Requests;
using Users.Core.Services;
using Users.Core.Helpers;
using Users.Core.Models.Users;
using Microsoft.Extensions.Options;
using Users.Core.Repositories;
using SharedKernel.Models.Settings;

namespace Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepository;
        private readonly TokenSettings _tokenSettings;

        public UserService(IUserRepository usersRepository, IOptions<TokenSettings> tokenSettings)
        {
            _usersRepository = usersRepository;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<Result<string>> LoginAsync(LoginRequest request)
        {
            return await Result
                .Create(request)
                .MapAsync(request => _usersRepository.GetUserByEmail(x => x.Email == request.Email))
                .ValidateNotNullAsync(ResultComplete.OperationFailed, $"User with email {request.Email} is not found.", true)
                .ValidateAsync(user =>
                    PasswordHelper.VerifyPassword(request.Password, user.Credentials.Password),
                    ResultComplete.InvalidArgument,
                    $"Login for user with email {request.Email} failed.",
                    true)
                .MapAsync(user => TokenHelper.GenerateJwtToken(user.Email, user.Id, _tokenSettings));
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
