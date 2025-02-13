using DiabetesApp.Dto.UserPreferencesDtos;

namespace DiabetesApp.Services.Interfaces
{
    public interface IUserPreferencesService
    {
        Task<GetUserPreferencesDto> GetUserPreferencesAsync(string userId);
        Task UpdateUserPreferencesAsync(string userId, UpdateUserPreferencesDto dto);
    }
}
