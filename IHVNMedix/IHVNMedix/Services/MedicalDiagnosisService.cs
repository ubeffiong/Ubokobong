using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IHVNMedix.Models;
using System.Collections.Generic;
using System.Reflection;

namespace IHVNMedix.Services
{
    public class MedicalDiagnosisService : IMedicalDiagnosisService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly string _secretKey;


        public MedicalDiagnosisService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;

            _apiUrl = configuration["ServiceUrls:HealthServiceUrl"];
            _apiKey = configuration["ApiKeys:MedicalDiagnosis"];
            _secretKey = configuration["ApiKeys:SecretKey"];
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string authServiceUrl = _configuration["AuthServiceUrl"];
            string username = _configuration["SandboxUsername"];
            string password = _configuration["SandboxPassword"];


            // Calculate HMAC
            string uri = $"{authServiceUrl}/login";
            byte[] secretBytes = Encoding.UTF8.GetBytes(_secretKey);
            string computedHashString = "";

            using (HMACMD5 hmac = new HMACMD5(secretBytes))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(uri);
                byte[] computedHash = hmac.ComputeHash(dataBytes);
                computedHashString = Convert.ToBase64String(computedHash);
            }

            //HttpClient and headers
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", $"{_apiKey}:{computedHashString}");

            try
            {
                var response = await _httpClient.PostAsync(uri, new StringContent(""));
                response.EnsureSuccessStatusCode();

                string token = await response.Content.ReadAsStringAsync();
                return token;
            }
            catch (Exception ex)
            {
                // Handle exceptions when API request fails
                throw ex;
            }
        }

        public async Task<List<Symptoms>> LoadSymptomsAsync(string accessToken)
        {
            try
            {
                string apiUrl = $"{_apiUrl}/symptoms";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                var symptomsResponse = JsonConvert.DeserializeObject<ApiResponse<Symptoms>>(json);

                return symptomsResponse.Items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Diagnosis>> LoadDiagnosisAsync(List<int> selectedSymptoms, string gender, int DOB, string accessToken)
        {
            try
            {
                string apiUrl = $"{_apiUrl}/diagnosis";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Construct the request body with selected parameters
                var requestBody = new
                {
                    SymptomIds = selectedSymptoms,
                    Gender = gender.ToString(),
                    YearOfBirth = DOB
                };

                string requestBodyJson = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(requestBodyJson, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                var diagnosisResponse = JsonConvert.DeserializeObject<ApiResponse<Diagnosis>>(json);

                return diagnosisResponse.Items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<HealthIssueInfo>> LoadIssuesAsync(string accessToken)
        {
            try
            {
                string apiUrl = $"{_apiUrl}/issues";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                var issuesResponse = JsonConvert.DeserializeObject<ApiResponse<HealthIssueInfo>>(json);

                return issuesResponse.Items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}