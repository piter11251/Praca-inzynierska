using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.Models
{
    public class SugarEntryDto
    {
        public int SugarValue { get; set; }
        public DateTime EntryDate { get; set; }
        public int MealType { get; set; }
        public int MealMarker { get; set; }
    }
}
