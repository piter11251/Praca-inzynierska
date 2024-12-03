using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto;
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

        public async Task CreateEntry(CreateEntryDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if(!Enum.TryParse<MealMarker>(dto.MealMarker, true, out var mealMarker))
            {
                throw new Exception("Invalid MealMarkerValue");
            }
            var entry = new Entry
            {
                SugarValue = dto.SugarValue,
                MealTime = dto.MealTime,
                MealMarker = mealMarker
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
                throw new Exception("Invalid MealMarkerValue");
            }
            var entry = await _context.Entries
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null)
            {
                throw new NotFoundException("Nie odnaleziono takiego wpisu");
            }

            if (dto.SugarValue.HasValue)
            {
                entry.SugarValue = dto.SugarValue.Value;
            }

            if (dto.MealTime.HasValue)
            {
                entry.MealTime = dto.MealTime.Value;
            }

            if (mealMarker != null)
            {
                entry.MealMarker = mealMarker;
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
    }
}
