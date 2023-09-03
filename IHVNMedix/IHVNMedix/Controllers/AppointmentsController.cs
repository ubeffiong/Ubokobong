using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHVNMedix.Data;
using IHVNMedix.Models;
using AutoMapper;
using IHVNMedix.Repositories;
using Microsoft.Extensions.Logging;
using IHVNMedix.DTOs;

namespace IHVNMedix.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorsController> _logger;
        public AppointmentsController(IDoctorRepository doctorRepository,
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IMapper mapper, ILogger<DoctorsController> logger)
        {
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
            _logger = logger;
        }
        

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAppointmemtAsync();
            var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            // Populate related entities (e.g., Patient and Doctor) using AutoMapper
            foreach (var appointmentDto in appointmentDtos)
            {
                appointmentDto.Patient = _mapper.Map<PatientDto>(await _patientRepository.GetPatientByIdAsync(appointmentDto.PatientId));
                appointmentDto.Doctor = _mapper.Map<DoctorDto>(await _doctorRepository.GetDoctorByIdAsync(appointmentDto.DoctorId));
            }
            return View(appointmentDtos);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmemtByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
            return View(appointmentDto);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            //var doctor = _doctorRepository.GetDoctorByIdAsync(id).Result;
            var patients = _patientRepository.GetAllPatientsAsync().Result;
            var doctors = _doctorRepository.GetAllDoctorsAsync().Result;

            // Map your entities to ViewModel if needed (using AutoMapper)
            var patientDtos = _mapper.Map<List<PatientDto>>(patients);
            var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);

            //Create a new appointment
            var viewModel = new AppointmentDto
            {
                YourListOfPatients = new SelectList(patientDtos, "Id", "FirstName"),
                YourListOfDoctors = new SelectList(doctorDtos, "Id", "FirstName")
            };
            //var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
            return View(viewModel);
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DoctorId,AppointmentDateTime,Specialty")] AppointmentDto appointmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Map the AppointmentDto back to your Appointment entity
                    var appointment = _mapper.Map<Appointment>(appointmentDto);

                    // Add the appointment to the repository
                    await _appointmentRepository.AddAppointmentAsync(appointment);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that might occur during appointment creation
                    ModelState.AddModelError("", "An error occurred while booking the appointment. Please try again later.");
                    // Log the exception for debugging purposes
                    _logger.LogError(ex, "Error booking appointment.");
                }

            }

            // If the model is not valid, return to the booking form with validation errors
            var patients = _patientRepository.GetAllPatientsAsync().Result;
            var doctors = _doctorRepository.GetAllDoctorsAsync().Result;

            // Map your entities to ViewModel if needed (using AutoMapper)
            var patientDtos = _mapper.Map<List<PatientDto>>(patients);
            var doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);

            // Populate the dropdowns with the updated data
            appointmentDto.YourListOfPatients = new SelectList(patientDtos, "Id", "FirstName");
            appointmentDto.YourListOfDoctors = new SelectList(doctorDtos, "Id", "FirstName");

            return View(appointmentDto);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmemtByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            // Fetch patient and doctor details based on the PatientId and DoctorId in the appointment
            var patient = await _patientRepository.GetPatientByIdAsync(appointment.PatientId);
            var doctor = await _doctorRepository.GetDoctorByIdAsync(appointment.DoctorId);

            appointmentDto.Patient = _mapper.Map<PatientDto>(patient);
            appointmentDto.Doctor = _mapper.Map<DoctorDto>(doctor);

            return View(appointmentDto);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,DoctorId,AppointmentDateTime,Specialty")] AppointmentDto appointmentDto)
        {
            if (id != appointmentDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var appointment = _mapper.Map<Appointment>(appointmentDto);
                    await _appointmentRepository.UpdateAppointmentAsync(appointment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while editing the appointment. Please try again later.");
                    _logger.LogError(ex, "Error editing appointment.");
                }
            }

            return View(appointmentDto);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmemtByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            // Fetch patient and doctor details based on the PatientId and DoctorId in the appointment
            var patient = await _patientRepository.GetPatientByIdAsync(appointment.PatientId);
            var doctor = await _doctorRepository.GetDoctorByIdAsync(appointment.DoctorId);

            appointmentDto.Patient = _mapper.Map<PatientDto>(patient);
            appointmentDto.Doctor = _mapper.Map<DoctorDto>(doctor);

            return View(appointmentDto);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
