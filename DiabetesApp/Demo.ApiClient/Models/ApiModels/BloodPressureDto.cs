using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ApiClient.Models.ApiModels
{
    public class BloodPressureDto
    {
        public int Id { get; set; }
        public DateTime MeasurementDate { get; set; }
        public int StolicPressure { get; set; }
        public int DiastolicPressure { get; set; }
        public int Pulse { get; set; }
        public bool IsIrregularPulse { get; set; } = false;
    }
}
