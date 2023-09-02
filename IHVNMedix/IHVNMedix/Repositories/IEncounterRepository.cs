using IHVNMedix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public interface IEncounterRepository
    {
        Task<IEnumerable<Encounter>> GetAllEncountersAsync();
        Task<Encounter> GetEncounterByIdAsync(int id);
        Task AddEncounterAsync(Encounter encounter);
        Task UpdateEncounterAsync(Encounter encounter);
        Task DeleteEncounterAsync(int id);
        Task<bool> EncounterExistsAsync(int id);
    }
}
