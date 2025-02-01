using DiabetesApp.Dto.BloodPressureDtos;

namespace DiabetesApp.Services.Interfaces
{
    public interface IBloodPressureService
    {
        Task CreateBloodPressureEntry(CreateBloodPressureEntryDto dto, string userId);
        Task ModifyBloodPressureEntry(ModifyBloodPressureDto dto, int id);
        Task DeleteBloodPressureEntry(int id);
        Task<List<GetBloodPressureDto>> GetAllBloodPressureEntries(string userId, int days);
        Task<GetBloodPressureDto> GetBloodPressureById(int id);
    }
}
