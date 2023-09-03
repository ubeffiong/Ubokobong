using System;
using System.Collections;
using System.Collections.Generic;

namespace IHVNMedix.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string DiagnosisResult { get; set; }
        public bool IsValid { get; set; }


        //public ICollection<Symptoms> Symptoms { get; set;}

    }
}
