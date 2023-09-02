using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHVNMedix.Data;
using IHVNMedix.Models;
using IHVNMedix.Repositories;
using AutoMapper;
using IHVNMedix.DTOs;

namespace IHVNMedix.Controllers
{
    public class EncountersController : Controller
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper _mapper;

        public EncountersController(IEncounterRepository encounterRepository, IMapper mapper)
        {
            _encounterRepository = encounterRepository;
            _mapper = mapper;
        }

        // GET: Encounters
        public async Task<IActionResult> Index()
        {
            var encounters = await _encounterRepository.GetAllEncountersAsync();
            var encounterDtos = _mapper.Map<IEnumerable<EncounterDto>>(encounters);
            return View(encounterDtos);
        }

        // GET: Encounters/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var encounterDtos = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDtos);
        }

        // GET: Encounters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Encounters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,EncounterDate")] Encounter encounter)
        {
            if (ModelState.IsValid)
            {
                await _encounterRepository.AddEncounterAsync(encounter);
                return RedirectToAction(nameof(Index));
            }
            var encounterDto = _mapper.Map<Encounter>(encounter);
            return View(encounterDto);
        }

        // GET: Encounters/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var encounterDto = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDto);
        }

        // POST: Encounters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,EncounterDate")] Encounter encounter)
        {
            if (id != encounter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _encounterRepository.UpdateEncounterAsync(encounter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EncounterExistsAsync(encounter.Id))
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
            var encounterDto = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDto);
        }

        // GET: Encounters/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var encounterDto = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDto);
        }

        // POST: Encounters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _encounterRepository.DeleteEncounterAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EncounterExistsAsync(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            return encounter !=null;
        }
    }
}
