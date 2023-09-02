using IHVNMedix.Data;
using IHVNMedix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public class EncounterRepository : IEncounterRepository
    {
        private readonly ApplicationDbContext _context;
        public EncounterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Encounter>> GetAllEncountersAsync()
        {
            return await _context.Encounters.ToListAsync();
        }
        public async Task<Encounter> GetEncounterByIdAsync(int id)
        {
            return await _context.Encounters.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddEncounterAsync(Encounter encounter)
        {
            _context.Encounters.Add(encounter);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEncounterAsync(Encounter encounter)
        {
            _context.Entry(encounter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEncounterAsync(int id)
        {
            var encounter = _context.Encounters.Find(id);
            if (encounter != null)
            {
                _context.Encounters.Remove(encounter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EncounterExistsAsync(int id)
        {
            return await _context.Encounters.AnyAsync(p => p.Id == id);
        }
    }
}
