using IHVNMedix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public interface ISymptomsRepository
    {
        Task<IEnumerable<Symptoms>> GetAllSymptomsAsync();
        Task<Symptoms> GetSymptomsByIdAsync(int id);
        Task AddSymptomsAsync(Symptoms symptoms);
        Task UpdateSymptomsAsync(Symptoms Symptoms);
        Task DeleteSymptomsAsync(int id);
        //Task<bool> SymptomsExistsAsync(int id);
    }
}
