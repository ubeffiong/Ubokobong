using IHVNMedix.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace IHVNMedix.DTOs
{
    public class AppointmentDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Patient Id is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doctor Id is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Appointment Date and Time Id is required")]
        public DateTime AppointmentDateTime { get; set; }
        [Required(ErrorMessage = "Specialty Id is required")]
        public string Specialty { get; set; }
    }
}
