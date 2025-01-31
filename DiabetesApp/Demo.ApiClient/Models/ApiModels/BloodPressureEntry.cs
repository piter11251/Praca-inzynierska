using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Demo.ApiClient.Models.ApiModels
{
    public class BloodPressureEntry
    {
        public int Id { get; set; }
        public int StolicValue { get; set; }
        public int DiastolicValue { get; set; }
        public DateTime MeasureTime { get; set; }
    }
}
