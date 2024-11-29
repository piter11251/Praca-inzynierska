using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DiabetesType DiabetesType { get; set; }

        public ICollection<Entry> Entries { get; set; }
        public ICollection<Pressure> Pressures { get; set; }
        public UserPreferences UserPreferences { get; set; }
    }
}
