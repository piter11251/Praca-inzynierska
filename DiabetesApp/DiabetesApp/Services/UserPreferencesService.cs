using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.UserPreferencesDtos;
using DiabetesApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.Services
{
    public class UserPreferencesService: IUserPreferencesService
    {
        private readonly DiabetesDbContext _context;
        public UserPreferencesService(DiabetesDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserPreferencesDto> GetUserPreferencesAsync(string userId)
        {
            var userPrefs = await _context.UserPreferences
                .Include(up => up.PreferableSugarLevels)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPrefs == null)
                return null;

            var dto = new GetUserPreferencesDto
            {
                PrefelableSugarLevels = userPrefs.PreferableSugarLevels
                .Select(level => new PreferableSugarLevelDto
                {
                    Id = level.Id,
                    MealMarker = level.MealMarker.ToString(),
                    MinValue = level.MinValue,
                    MaxValue = level.MaxValue
                })
                .ToList()
            };
            return dto;
        }

        public async Task UpdateUserPreferencesAsync(string userId, UpdateUserPreferencesDto dto)
        {
            var userPrefs = await _context.UserPreferences
                .Include(up => up.PreferableSugarLevels)
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPrefs == null)
                return;

            foreach(var levelDto in dto.PreferableSugarLevels)
            {
                var level = userPrefs.PreferableSugarLevels
                    .FirstOrDefault(x => x.Id == levelDto.Id);

                if(level == null) continue;

                level.MinValue = levelDto.MinValue;
                level.MaxValue = levelDto.MaxValue;
            }
            await _context.SaveChangesAsync();
        }
    }
}
