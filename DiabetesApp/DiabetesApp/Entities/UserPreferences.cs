using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Entities
{
    public class UserPreferences
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<PreferableSugarLevel> PreferableSugarLevels { get; set; }
        public User User { get; set; }

        public UserPreferences()
        {
            PreferableSugarLevels = new List<PreferableSugarLevel>
            {
                new PreferableSugarLevel
                {
                    MealMarker = MealMarker.On_Empty_Stomach,
                    MinValue = 70,
                    MaxValue = 110
                },
                new PreferableSugarLevel
                {
                    MealMarker = MealMarker.Before_Meal,
                    MinValue = 70,
                    MaxValue = 110
                },
                new PreferableSugarLevel()
                {
                    MealMarker = MealMarker.After_Meal,
                    MinValue = 70,
                    MaxValue = 180
                }

            };
        }
    }
}
