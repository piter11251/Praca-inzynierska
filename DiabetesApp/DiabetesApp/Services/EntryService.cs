using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto;
using DiabetesApp.Entities;
using DiabetesApp.Services.Interfaces;

namespace DiabetesApp.Services
{
    public class EntryService: IEntryService
    {
        private readonly DiabetesDbContext _context;
        public EntryService(DiabetesDbContext context)
        {
            _context = context;
        }

        public async Task CreateEntry(CreateEntryDto dto)
        {
            var entry = new Entry
            {
                SugarValue = dto.SugarValue,
                MealTime = dto.MealTime,
                MealMarker = dto.MealMarker
            };

            await _context.Entries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }
}
