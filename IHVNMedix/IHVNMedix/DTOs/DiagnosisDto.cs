using IHVNMedix.Models;
using System;

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

    }
}
