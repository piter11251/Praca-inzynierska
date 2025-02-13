using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Enums
{
    public enum Gender
    {
        Man,
        Woman
    }

    public static class GenderEnum
    {
        //public static List<string> Values => Enum.GetNames(typeof(Gender)).ToList();
        public static List<string> Values = new List<string>() { "Mężczyzna", "Kobieta" };
        public static string FriendlyNames(Gender gender)
        {
            return gender switch
            {
                Gender.Man => "Mężczyzna",
                Gender.Woman => "Kobieta"
            };
        }
    }
}
