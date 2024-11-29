using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Entities
{
    public class PreferableSugarLevel
    {
        public int Id { get; set; }
        public int UserPreferencesId { get; set; }
        public MealMarker MealMarker { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public UserPreferences UserPrefences { get; set; }
    }
}
