using Demo.ApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public class SugarEntryDisplayModel
    {
        public int Id { get; set; }
        public int SugarValue { get; set; }
        public DateTime MealTime { get; set; }
        public string MealMarker { get; set; }
        public Color LabelColor { get; set; }
    }
}
