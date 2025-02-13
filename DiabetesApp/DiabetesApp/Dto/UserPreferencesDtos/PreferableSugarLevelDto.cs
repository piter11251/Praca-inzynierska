namespace DiabetesApp.Dto.UserPreferencesDtos
{
    public class PreferableSugarLevelDto
    {
        public int Id { get; set; }
        public string MealMarker { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
