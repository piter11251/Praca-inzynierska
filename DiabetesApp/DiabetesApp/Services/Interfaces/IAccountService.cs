using DiabetesApp.Dto.Account;

namespace DiabetesApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
        Task<UserProfileDto> GetUserProfileAsync(string userId);
    }
}
