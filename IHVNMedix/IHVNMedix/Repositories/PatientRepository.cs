using IHVNMedix.Models;
using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using IHVNMedix.Data;

namespace IHVNMedix.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            try
            {
                //patient.Id = GenerateHashedId(); // Generate a hashed ID - Change patient Id to string in patient model
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Access the inner exception for details
                var innerException = ex.InnerException;
                // Log or handle the inner exception
                throw; // Rethrow the exception or handle it as needed
            }
            
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            try
            {
                _context.Entry(patient).State = EntityState.Modified;
                // Ensure that the ID remains unchanged
                //_context.Entry(patient).Property(x => x.Id).IsModified = false;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Access the inner exception for details
                var innerException = ex.InnerException;
                // Log or handle the inner exception
                throw; // Rethrow the exception or handle it as needed
            }
            
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PatientExistsAsync(int id)
        {
            return await _context.Patients.AnyAsync(p => p.Id == id);
        }

        private string GenerateHashedId()
        {
            // Generate a unique hashed ID, e.g., using a hashing algorithm like SHA-256
            // You can use a library like System.Security.Cryptography for this purpose
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Guid.NewGuid().ToByteArray());
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
