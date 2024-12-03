using DiabetesApp.Dto;
using DiabetesApp.Entities;

namespace DiabetesApp.Services.Interfaces
{
    public interface IEntryService
    {
        Task CreateEntry(CreateEntryDto dto);
        Task ModifyEntry(ModifyEntryDto dto, int id);
        Task DeleteEntry(int id);
    }
}
