using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Enums
{
    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }
    public static class MealTypeEnum
    {
        public static List<string> Values = new List<string> { "Sniadanie", "Obiad", "Kolacja", "Przekaska" };
        public static string FriendlyNames(MealType type)
        {
            return type switch
            {
                MealType.Breakfast => "Sniadanie",
                MealType.Lunch => "Obiad",
                MealType.Dinner => "Kolacja",
                MealType.Snack => "Przekaska"
            };
        }
    }
}
