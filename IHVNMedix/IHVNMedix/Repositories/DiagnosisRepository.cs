using IHVNMedix.Data;
using IHVNMedix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly ApplicationDbContext _context;

        public DiagnosisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Diagnosis>> GetAllDiagnosesAsync()
        {
            return await _context.Diagnosis.ToListAsync();
        }

        public async Task<Diagnosis> GetDiagnosisByIdAsync(int id)
        {
            return await _context.Diagnosis.FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task AddDiagnosisAsync(Diagnosis diagnosis)
        {
            _context.Diagnosis.Add(diagnosis);
            return Task.CompletedTask;
        }

        public async Task UpdateDiagnosisAsync(Diagnosis diagnosis)
        {
            _context.Entry(diagnosis).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDiagnosisAsync(int id)
        {
            var diagnosis = await _context.Diagnosis.FindAsync(id);
            if (diagnosis != null)
            {
                _context.Diagnosis.Remove(diagnosis);
                await _context.SaveChangesAsync();
            }
        }

    }
}
