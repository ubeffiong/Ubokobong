using IHVNMedix.Models;
using System.Collections.Generic;
using System;

namespace IHVNMedix.DTOs
{
    public class EncounterDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime EncounterDate { get; set; }
        public ICollection<Symptoms> Symptoms { get; set; }
        public ICollection<VitalSigns> VitalSigns { get; set; }

    }
}
