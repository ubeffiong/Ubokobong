using IHVNMedix.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //public ICollection<HealthItem> SymptomsResults { get; set; } // Initial and Symptoms from the API
        //public ICollection<Diagnosis> DiagnosisResults { get; set; } // Diagnosis results from the API

        // Add properties for selected symptoms and diagnosis results
        public List<int> SelectedSymptoms { get; set; }
        public List<Diagnosis> DiagnosisResults { get; set; }

        public List<SelectListItem> YourListOfSymptoms { get; set; }
        public string PatientName { get; internal set; }
    }
}
