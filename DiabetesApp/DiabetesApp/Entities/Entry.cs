using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Entities
{
    public class Entry
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SugarValue { get; set; }
        public DateTime MealTime { get; set; }
        public MealMarker MealMarker { get; set; }
        public MealType? MealType { get; set; }

        public User User { get; set; }
    }
}
