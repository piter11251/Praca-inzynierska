

using DiabetesApp.Dto;

namespace DiabetesApp.Services.Interfaces
{
    public interface IBloodPressureService
    {
        Task CreateBloodPressureEntry(CreateBloodPressureEntryDto dto);
        Task ModifyBloodPressureEntry(ModifyBloodPressureDto dto, int id);
        Task DeleteBloodPressureEntry(int id);
        Task<List<GetBloodPressureDto>> GetAllBloodPressureEntries();
    }
}
