using System.Collections;
using System.Collections.Generic;

namespace IHVNMedix.Models
{
    public class Symptoms
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int EncounterID { get; set; }
        public Encounter Encounter { get; set; }
        //public ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
