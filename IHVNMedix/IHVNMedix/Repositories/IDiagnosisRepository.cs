using IHVNMedix.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public interface IDiagnosisRepository
    {
        Task<IEnumerable<Diagnosis>> GetAllDiagnosesAsync();
        Task<Diagnosis> GetDiagnosisByIdAsync(int id);
        Task AddDiagnosisAsync(Diagnosis diagnosis);
        Task UpdateDiagnosisAsync(Diagnosis diagnosis);
        Task DeleteDiagnosisAsync(int id);
    }
}
