using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Enums
{
    public enum MealMarker
    {
        On_Empty_Stomach,
        Before_Meal,
        After_Meal
    }
    public static class MealMarkerEnum
    {
        public static List<string> Values = new List<string> { "Na czczo", "Przed posilkiem", "Po posilku" };
        public static string FriendlyNames(MealMarker mealMarker)
        {
            return mealMarker switch
            {
                MealMarker.On_Empty_Stomach => "Na czczo",
                MealMarker.Before_Meal => "Przed posilkiem",
                MealMarker.After_Meal => "Po posilku"
            };
        }
    }
}
    
