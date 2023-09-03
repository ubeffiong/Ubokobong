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
using System.Numerics;
using IHVNMedix.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace IHVNMedix.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly IDiagnosisRepository _diagnosisRepository;
        private readonly IMapper _mapper;
        private readonly MedicalDiagnosisService _diagnosisService;
        private readonly IConfiguration _configuration;

        public DiagnosesController(IDiagnosisRepository diagnosisRepository, IMapper mapper, MedicalDiagnosisService diagnosisService, IConfiguration configuration)
        {
            _diagnosisRepository = diagnosisRepository;
            _mapper = mapper;
            _diagnosisService = diagnosisService;
            _configuration = configuration;
        }

        // GET: Diagnoses
        public async Task<IActionResult> Index()
        {
            var diagnoses = await _diagnosisRepository.GetAllDiagnosesAsync();
            var diagnosisDtos = _mapper.Map<IEnumerable<DoctorDto>>(diagnoses);
            return View(diagnosisDtos);
        }

        // GET: Diagnoses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var diagnosis = await _diagnosisRepository.GetDiagnosisByIdAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }
            var diagnosisDto = _mapper.Map<DoctorDto>(diagnosis);
            return View(diagnosisDto);
        }

        // GET: Diagnoses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnoses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DoctorId,DiagnosisDate,DiagnosisResult,IsValid")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                await _diagnosisRepository.AddDiagnosisAsync(diagnosis);
                return RedirectToAction(nameof(Index));
            }
            var diagnosisDto = _mapper.Map<DoctorDto>(diagnosis);
            return View(diagnosisDto);
        }

        // GET: Diagnoses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var diagnosis = await _diagnosisRepository.GetDiagnosisByIdAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }
            var diagnosisDto = _mapper.Map<DoctorDto>(diagnosis);
            return View(diagnosisDto);
        }

        // POST: Diagnoses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,DoctorId,DiagnosisDate,DiagnosisResult,IsValid")] Diagnosis diagnosis)
        {
            if (id != diagnosis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _diagnosisRepository.UpdateDiagnosisAsync(diagnosis);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DiagnosisExistsAsync(diagnosis.Id))
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
            var diagnosisDto = _mapper.Map<DoctorDto>(diagnosis);
            return View(diagnosisDto);
        }

        // GET: Diagnoses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var diagnosis = await _diagnosisRepository.GetDiagnosisByIdAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }
            var diagnosisDto = _mapper.Map<DoctorDto>(diagnosis);
            return View(diagnosisDto);
        }

        // POST: Diagnoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _diagnosisRepository.DeleteDiagnosisAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool>DiagnosisExistsAsync(int id)
        {
            var diagnosis = await _diagnosisRepository.GetDiagnosisByIdAsync(id);
            return diagnosis != null;
        }

        public async Task<IActionResult> GetDiagnosis()
        {
            try
            {
                // Get an access token
                string accessToken = await _diagnosisService.GetAccessTokenAsync();

                // Use the access token to make API requests
                if (!string.IsNullOrEmpty(accessToken))
                {
                    // Example: Make a sample API request to load symptoms
                    var symptoms = await _diagnosisService.LoadSymptomsAsync(accessToken);

                    // Handle the API response (in this example, we're just returning it as JSON)
                    return Json(symptoms);
                }
                else
                {
                    // Handle the case where access token retrieval failed
                    return BadRequest("Failed to retrieve access token.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as network errors or API request failures
                return BadRequest($"API request failed: {ex.Message}");
            }
            //return View();
        }

        public async Task<IActionResult> LoadSymptoms()
        {
            string accessToken = await _diagnosisService.GetAccessTokenAsync();
            var symptoms = await _diagnosisService.LoadSymptomsAsync(accessToken);

            // Handle symptoms (e.g., display in a view)
            return View(symptoms);
        }

        public async Task<IActionResult> LoadDiagnosis(List<int> selectedSymptoms, string gender, int yearOfBirth)
        {
            string accessToken = await _diagnosisService.GetAccessTokenAsync();
            var diagnosis = await _diagnosisService.LoadDiagnosisAsync(selectedSymptoms, gender, yearOfBirth, accessToken);

            // Handle diagnosis results (e.g., display in a view)
            return View(diagnosis);
        }

        public async Task<IActionResult> LoadIssues()
        {
            string accessToken = await _diagnosisService.GetAccessTokenAsync();
            var issues = await _diagnosisService.LoadIssuesAsync(accessToken);

            // Handle issues (e.g., display in a view)
            return View(issues);
        }
    }
}
