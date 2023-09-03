using IHVNMedix.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace IHVNMedix.DTOs
{
    public class DiagnosisDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string DiagnosisResult { get; set; }
        public bool IsValid { get; set; }

        //DiagnosisRequest
        public List<int> SymptomIds { get; set; }
        public string Gender { get; set; }
        public int YearOfBirth { get; set; }

        public List<int> SelectedSymptoms { get; set; } // Change this property to List<int>
        public List<SelectListItem> YourListOfSymptoms { get; set; }

    }
}
