namespace DiabetesApp.Dto.EntryDtos
{
    public class GetEntryDto
    {
        public int SugarValue { get; set; }
        public DateTime MealTime { get; set; }
        public string MealMarker { get; set; }
    }
}
