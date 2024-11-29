namespace DiabetesApp.Entities
{
    // Ciśnienie
    public class Pressure
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StolicPressure { get; set; } // ciśnienie skurczowe
        public int DiaStolicPressure { get; set; } // ciśnienie rozkurczowe
        public int Pulse {  get; set; }
        public bool IsIrregularPulse { get; set; } = false;
        public DateTime MeasureDate { get; set; }
        
        public User User { get; set; }
    }
}
