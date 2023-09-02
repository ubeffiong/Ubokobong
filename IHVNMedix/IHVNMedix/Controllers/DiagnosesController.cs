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

namespace IHVNMedix.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly IDiagnosisRepository _diagnosisRepository;
        private readonly IMapper _mapper;

        public DiagnosesController(IDiagnosisRepository diagnosisRepository, IMapper mapper)
        {
            _diagnosisRepository = diagnosisRepository;
            _mapper = mapper;
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
    }
}
