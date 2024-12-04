using DiabetesApp.Entities.Enum;
using Microsoft.AspNetCore.Identity;

namespace DiabetesApp.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DiabetesType DiabetesType { get; set; }

        public ICollection<Entry> Entries { get; set; }
        public ICollection<BloodPressure> Pressures { get; set; }
        public UserPreferences UserPreferences { get; set; }
    }
}
