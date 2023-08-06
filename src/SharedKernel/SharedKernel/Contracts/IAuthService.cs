namespace SharedKernel.Contracts
{
    public interface IAuthService
    {
        public string GetUserId { get; }

        public Task<string> GetUserAccessToken();
    }
}
