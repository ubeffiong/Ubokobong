using IHVNMedix.DTOs;
using System.Collections.Generic;

namespace IHVNMedix.Models
{
    public class PatientRegistrationViewModel
    {
        public IEnumerable<PatientDto> Patients { get; set; }
        public PatientDto NewPatient { get; set; }
    }
}
