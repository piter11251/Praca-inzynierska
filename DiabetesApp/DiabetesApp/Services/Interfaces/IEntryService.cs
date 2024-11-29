using DiabetesApp.Dto;
using DiabetesApp.Entities;

namespace DiabetesApp.Services.Interfaces
{
    public interface IEntryService
    {
        Task CreateEntry(CreateEntryDto entry);
    }
}
