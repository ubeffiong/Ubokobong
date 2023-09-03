using IHVNMedix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public interface IVitalSignsRepository
    {
        Task<IEnumerable<VitalSigns>> GetAllVitalSignsAsync();
        Task<VitalSigns> GetVitalSignsByIdAsync(int id);
        Task AddVitalSignsAsync(VitalSigns vitalSigns);
        Task UpdateVitalSignsAsync(VitalSigns vitalSigns);
        Task DeleteVitalSignsAsync(int id);

        Task<IEnumerable<VitalSigns>> GetVitalSignsByEncounterIdAsync(int encounterId);
        //Task<bool> PatientExistsAsync(int id);
    }
}
