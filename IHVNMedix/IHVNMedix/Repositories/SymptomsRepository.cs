using IHVNMedix.Data;
using IHVNMedix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public class SymptomsRepository : ISymptomsRepository
    {
        private readonly ApplicationDbContext _context;

        public SymptomsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSymptomsAsync(Symptoms symptoms)
        {
            _context.Symptoms.Add(symptoms);
            await _context.SaveChangesAsync();
        }

        public Task DeleteSymptomsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Symptoms>> GetAllSymptomsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Symptoms> GetSymptomsByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSymptomsAsync(Symptoms Symptoms)
        {
            throw new System.NotImplementedException();
        }
    }
}
