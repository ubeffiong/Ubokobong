using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHVNMedix.Models;
using IHVNMedix.Repositories;
using AutoMapper;
using IHVNMedix.DTOs;
using Microsoft.Extensions.Logging;

namespace IHVNMedix.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorsController> _logger;
        public DoctorsController(IDoctorRepository doctorRepository, 
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

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();
            var doctorDtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return View(doctorDtos);
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return View(doctorDto);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,EmailAddress,Specialty")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _doctorRepository.AddDoctorAsync(doctor);
                return RedirectToAction(nameof(Index));
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return View(doctorDto);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return View(doctorDto);
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,EmailAddress,Specialty")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _doctorRepository.UpdateDoctorAsync(doctor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DoctorExistsAsync(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return View(doctorDto);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return View(doctorDto);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorRepository.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DoctorExistsAsync(int id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            return doctor != null;
        }

        //GET: Doctor/BookAppointment/5
        public IActionResult BookAppointment()
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

        //POST: Doctor/BookAppointment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(int id, [Bind("PatientId,DoctorId, Specialty, AppointmentDateTime")] AppointmentDto appointmentDto)
        {
            if(ModelState.IsValid)
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
    }
}
