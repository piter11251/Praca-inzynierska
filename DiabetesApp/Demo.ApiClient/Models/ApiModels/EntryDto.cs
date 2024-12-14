using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ApiClient.Models.ApiModels
{
    public class EntryDto
    {
        public int Id { get; set; }
        public int SugarValue {  get; set; }
        public DateTime MealTime { get; set; }
        public string? MealMarker { get; set; }
    }
}
