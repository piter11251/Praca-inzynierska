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
        public static List<string> Values => Enum.GetNames(typeof(DiabetesType)).ToList();
    }
}
