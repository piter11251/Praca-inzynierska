using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Dto
{
    public class ModifyEntryDto
    {
        public int? SugarValue { get; set; }
        public DateTime? MealTime { get; set; }
        public string? MealMarker { get; set; }

    }
}
