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
        public static List<string> Values => Enum.GetNames(typeof(Gender)).ToList();
    }
}
