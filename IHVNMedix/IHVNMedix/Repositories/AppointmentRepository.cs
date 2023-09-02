using IHVNMedix.Data;
using IHVNMedix.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHVNMedix.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Appointment>> GetAllAppointmemtAsync()
        {
            return await _context.Appointments.ToListAsync();
        }
        public async Task<Appointment> GetAppointmemtByIdAsync(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAppointmentAsync(int id)
        {
            var appoiontment = await _context.Appointments.FindAsync(id);
            if (appoiontment == null)
            {
                _context.Appointments.Remove(appoiontment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
