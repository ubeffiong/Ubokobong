using IHVNMedix.Models;

namespace IHVNMedix.DTOs
{
    public class VitalSignsDto
    {
        public int Id { get; set; }
        public int EncounterId { get; set; }
        public Encounter Encounter { get; set; }
        public double Temperature { get; set; }
        public double Systolic { get; set; }
        public double Diatolic { get; set; }
        public double PulseRate { get; set; }
        public double RespiratoryRate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BMI { get; set; }
        public string Comment { get; set; }

    }
}
