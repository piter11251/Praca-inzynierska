using DiabetesApp.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabetesApp.Entities
{
    public class Entry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SugarValue { get; set; }
        public DateTime MealTime { get; set; }
        public MealMarker MealMarker { get; set; }
        public MealType? MealType { get; set; }

        public User User { get; set; }
    }
}
