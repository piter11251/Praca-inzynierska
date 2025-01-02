using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Enums
{
    public enum DiabetesType
    {
        Type_One,
        Type_Two
    }

    public static class DiabetesTypeEnum
    {
        public static List<string> Values => new List<string> { "Typ 1", "Typ 2" };
        public static string FriendlyNames(DiabetesType type)
        {
            return type switch
            {
                DiabetesType.Type_One => "Typ 1",
                DiabetesType.Type_Two => "Typ 2"
            };
        }
    }
}
