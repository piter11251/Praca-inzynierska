using DiabetesApp.Enums;

namespace DiabetesApp.Models
{
    public class GlucoseLevel
    {
        public int PatientId { get; set; }
        public int MeasurementId { get; set; }
        public int Glucose {  get; set; }
        public DateTime Date { get; set; }
        public MeasurementTime MeasurementTime { get; set; }
    }
}
