using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.EntryDtos;
using DiabetesApp.Entities;
using DiabetesApp.Entities.Enum;
using DiabetesApp.Exceptions;
using DiabetesApp.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.Services
{
    public class EntryService: IEntryService
    {
        private readonly DiabetesDbContext _context;
        private readonly IValidator<CreateEntryDto> _createValidator;
        private readonly IValidator<ModifyEntryDto> _modifyValidator;
        public EntryService(DiabetesDbContext context, IValidator<ModifyEntryDto> modifyValidator, IValidator<CreateEntryDto> createValidator)
        {
            _context = context;
;           _modifyValidator = modifyValidator;
            _createValidator = createValidator;
        }

        public async Task CreateEntry(CreateEntryDto dto, string userId)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            var entry = new Entry
            {
                UserId = userId,
                SugarValue = dto.SugarValue,
                MealTime = dto.MealTime,
                MealMarker = dto.MealMarker
            };

            await _context.Entries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task ModifyEntry(ModifyEntryDto dto, int id)
        {
            var validationResult = await _modifyValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if (!Enum.TryParse<MealMarker>(dto.MealMarker, true, out var mealMarker))
            {
                throw new BadRequestException("Niepoprawna wartość pory pomiaru");
            }
            var entry = await _context.Entries
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null)
            {
                throw new NotFoundException("Nie odnaleziono takiego wpisu");
            }

            entry.SugarValue = dto.SugarValue ?? entry.SugarValue;
            entry.MealTime = dto.MealTime ?? entry.MealTime;
            if (!string.IsNullOrEmpty(dto.MealMarker))
            {
                entry.MealMarker = Enum.Parse<MealMarker>(dto.MealMarker, true);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteEntry(int id)
        {
            var entry = await _context.Entries
                .FirstOrDefaultAsync (e => e.Id == id);
            if (entry == null)
            {
                throw new NotFoundException("Nie znaleziono wpisu");
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetEntryDto>> GetAllEntries()
        {
            var entries = await _context.Entries.ToListAsync();
            
            if(entries == null)
            {
                throw new NotFoundException("Nie znaleziono żadnych wpisów");
            }

            return entries.Select(entry => new GetEntryDto
            {
                SugarValue = entry.SugarValue,
                MealTime = entry.MealTime,
                MealMarker = Enum.GetName(typeof(MealMarker), entry.MealMarker)
            }).ToList();

        }

        public async Task<GetEntryDto> GetEntryById(int id)
        {
            var entry = await _context.Entries.FirstOrDefaultAsync(e => e.Id == id);
            if(entry == null)
            {
                throw new NotFoundException("Nie ma takiego wpisu");
            }

            var entryDto = new GetEntryDto
            {
                SugarValue = entry.SugarValue,
                MealTime = entry.MealTime,
                MealMarker = Enum.GetName(typeof(MealMarker), entry.MealMarker)
            };

            return entryDto;
        }
    }
}
