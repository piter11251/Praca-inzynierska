using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ApiClient.Models.ApiModels
{
    public class SugarEntryDto
    {
        public int SugarValue { get; set; }
        public DateTime MealTime { get; set; }
        public int MealMarker { get; set; }
        public int MealType { get; set; }
    }
}
