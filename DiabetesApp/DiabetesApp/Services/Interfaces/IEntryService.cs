using DiabetesApp.Dto.EntryDtos;
using DiabetesApp.Entities;

namespace DiabetesApp.Services.Interfaces
{
    public interface IEntryService
    {
        Task CreateEntry(CreateEntryDto dto);
        Task ModifyEntry(ModifyEntryDto dto, int id);
        Task DeleteEntry(int id);
        Task<List<GetEntryDto>> GetAllEntries();
        Task<GetEntryDto> GetEntryById(int id);
    }
}
