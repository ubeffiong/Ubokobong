using IHVNMedix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Services
{
    public interface IMedicalDiagnosisService
    {
        Task<string> GetAccessTokenAsync();
        Task<List<Symptoms>> LoadSymptomsAsync(string accessToken);
        Task<List<Diagnosis>> LoadDiagnosisAsync(List<int> selectedSymptoms, string gender, int yearOfBirth, string accessToken);
        Task<List<HealthIssueInfo>> LoadIssuesAsync(string accessToken);
    }
}
