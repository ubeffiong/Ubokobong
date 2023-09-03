using IHVNMedix.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IHVNMedix.DTOs
{
    public class AppointmentDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a patient.")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please select a doctor.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please select an appointment date and time.")]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Please specify a specialty.")]
        public string Specialty { get; set; }


        public SelectList YourListOfPatients { get; set; }
        public SelectList YourListOfDoctors { get; set; }

        
        public PatientDto Patient { get; set; } // Add a property for Patient information

        public DoctorDto Doctor { get; set; } // Add a property for Doctor information if needed
        

    }
}
