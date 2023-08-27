namespace SharedKernel.Contracts
{
    public interface IAuthService
    {
        public string GetUserId { get; }

        public string GetUserEmail { get; }

        public Task<string> GetUserAccessToken();
    }
}
