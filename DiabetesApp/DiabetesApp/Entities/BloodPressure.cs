namespace DiabetesApp.Entities
{
    // Ciśnienie
    public class BloodPressure
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StolicPressure { get; set; } // ciśnienie skurczowe
        public int DiastolicPressure { get; set; } // ciśnienie rozkurczowe
        public int Pulse {  get; set; }
        public bool? IsIrregularPulse { get; set; } = false;
        public DateTime MeasureDate { get; set; }
        
        public User User { get; set; }
    }
}
