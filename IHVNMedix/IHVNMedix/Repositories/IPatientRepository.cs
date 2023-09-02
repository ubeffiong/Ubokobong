using IHVNMedix.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id); 
        Task<bool> PatientExistsAsync(int id);
    }
}
