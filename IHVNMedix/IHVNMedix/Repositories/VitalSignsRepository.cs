using IHVNMedix.Data;
using IHVNMedix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public class VitalSignsRepository : IVitalSignsRepository
    {
        private readonly ApplicationDbContext _context;

        public VitalSignsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddVitalSignsAsync(VitalSigns vitalSigns)
        {
            _context.VitalSigns.Add(vitalSigns);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<VitalSigns>> GetVitalSignsByEncounterIdAsync(int encounterId)
        {
            // Query the database to retrieve vital signs for the specified encounter ID
            return await _context.VitalSigns
                .Where(v => v.EncounterId == encounterId)
                .ToListAsync();
        }

        public async Task DeleteVitalSignsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<VitalSigns>> GetAllVitalSignsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<VitalSigns> GetVitalSignsByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateVitalSignsAsync(VitalSigns vitalSigns)
        {
            throw new System.NotImplementedException();
        }
    }
}
