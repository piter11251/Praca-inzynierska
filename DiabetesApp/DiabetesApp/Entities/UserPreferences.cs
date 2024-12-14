using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Entities
{
    public class UserPreferences
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<PreferableSugarLevel> PreferableSugarLevels { get; set; }
        public User User { get; set; }
    }
}
