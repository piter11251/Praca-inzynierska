namespace DiabetesApp.Dto.BloodPressureDtos
{
    public class GetBloodPressureDto
    {
        public int Id { get; set; }
        public DateTime MeasurementDate { get; set; }
        public int StolicPressure { get; set; }
        public int DiastolicPressure { get; set; }
        public int Pulse { get; set; }
        public bool IsIrregularPulse { get; set; }
    }
}
