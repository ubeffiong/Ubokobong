using IHVNMedix.Models;

namespace IHVNMedix.DTOs
{
    public class SymptomsDto
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public int EncounterID { get; set; }
        public Encounter Encounter { get; set; }
        //public ICollection<Diagnosis> Diagnoses { get; set; }

    }
}
