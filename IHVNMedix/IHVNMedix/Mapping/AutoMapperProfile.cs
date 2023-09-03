using AutoMapper;
using IHVNMedix.DTOs;
using IHVNMedix.Models;

namespace IHVNMedix.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<Encounter, EncounterDto>();
            CreateMap<Symptoms, SymptomsDto>();
            CreateMap<VitalSigns, VitalSignsDto>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<Diagnosis, DiagnosisDto>();
            CreateMap<AppointmentDto, Appointment>(); //this added to map data from DTO to data model for processing data received from the client
        }

    }
}
