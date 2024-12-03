using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.BloodPressureDtos;
using DiabetesApp.Entities;
using DiabetesApp.Exceptions;
using DiabetesApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.Services
{
    public class BloodPressureService: IBloodPressureService
    {
        private readonly DiabetesDbContext _context;
        public BloodPressureService(DiabetesDbContext context)
        {
            _context = context;
        }

        public async Task CreateBloodPressureEntry(CreateBloodPressureEntryDto dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException("Prosze wypelnic formularz");
            }
            var entry = new BloodPressure
            {
                StolicPressure = dto.StolicPressure,
                DiastolicPressure = dto.DiastolicPressure,
                Pulse = dto.Pulse,
                IsIrregularPulse = dto.IsIrregularPulse,
                MeasureDate = dto.MeasurementDate
            };

            _context.Pressures.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task ModifyBloodPressureEntry(ModifyBloodPressureDto dto, int id)
        {
            var entry = await _context.Pressures.FirstOrDefaultAsync(p => p.Id == id);
            if(entry == null)
            {
                throw new NotFoundException("Nie znaleziono tego pomiaru");
            }

            if (dto.StolicPressure.HasValue) entry.StolicPressure = dto.StolicPressure.Value;
            if (dto.DiastolicPressure.HasValue) entry.DiastolicPressure = dto.DiastolicPressure.Value;
            if (dto.Pulse.HasValue) entry.Pulse = dto.Pulse.Value;
            if (dto.IsIrregularPulse.HasValue) entry.IsIrregularPulse = dto.IsIrregularPulse.Value;
            if (dto.MeasurementDate.HasValue) entry.MeasureDate = dto.MeasurementDate.Value;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteBloodPressureEntry(int id)
        {
            var entry = await _context.Pressures.FirstOrDefaultAsync(p => p.Id == id);
            if(entry == null)
            {
                throw new Exception("Nie ma takiego wpisu");
            }

            _context.Pressures.Remove(entry);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<GetBloodPressureDto>> GetAllBloodPressureEntries()
        {
            var entries = await _context.Pressures.ToListAsync();
            var newEntries = new List<GetBloodPressureDto>();
            if(entries == null)
            {
                throw new NotFoundException("Nie ma zadnych pomiarów");
            }

            foreach(var entry in entries)
            {
                var entryDto = new GetBloodPressureDto
                {
                    DiastolicPressure = entry.DiastolicPressure,
                    StolicPressure = entry.StolicPressure,
                    Pulse = entry.Pulse,
                    MeasurementDate = entry.MeasureDate
                };
                if(entry.IsIrregularPulse.HasValue)
                    entryDto.IsIrregularPulse = entry.IsIrregularPulse.Value;
                newEntries.Add(entryDto);
            }

            return newEntries;
        }

        public async Task<GetBloodPressureDto> GetBloodPressureById(int id)
        {
            var bloodPressure = await _context.Pressures.FirstOrDefaultAsync(x => x.Id == id);
            if(bloodPressure == null)
            {
                throw new NotFoundException("Nie znaleziono pomiaru");
            }

            var entryDto = new GetBloodPressureDto
            {
                StolicPressure = bloodPressure.StolicPressure,
                DiastolicPressure = bloodPressure.DiastolicPressure,
                Pulse = bloodPressure.Pulse,
                MeasurementDate = bloodPressure.MeasureDate
            };
            if (bloodPressure.IsIrregularPulse.HasValue)
                entryDto.IsIrregularPulse = bloodPressure.IsIrregularPulse.Value;

            return entryDto;
        }
    }
}
