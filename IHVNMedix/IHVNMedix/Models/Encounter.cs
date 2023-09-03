using System;
using System.Collections;
using System.Collections.Generic;

namespace IHVNMedix.Models
{
    public class Encounter
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime EncounterDate { get; set; }
        public ICollection<Symptoms> Symptoms { get; set; }
        public ICollection<VitalSigns> VitalSigns { get; set; }
        public ICollection<HealthItem> SymptomsResults { get; set; } // Initial and Symptoms from the API
        public ICollection<Diagnosis> DiagnosisResults { get; set; } // Diagnosis results from the API
    }
}
