using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Dto.EntryDtos
{
    public class ModifyEntryDto
    {
        public int? SugarValue { get; set; }
        public DateTime? MealTime { get; set; }
        public string? MealMarker { get; set; }

    }
}
