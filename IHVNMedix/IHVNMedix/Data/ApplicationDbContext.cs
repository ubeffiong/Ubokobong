using IHVNMedix.Models;
using Microsoft.EntityFrameworkCore;

namespace IHVNMedix.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<VitalSigns> VitalSigns { get; set; }
        public DbSet<Symptoms> Symptoms { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //Configuration one-to-many relationship between Patient and Encounter
            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.Patient)
                .WithMany(p => p.Encounters)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SymptomDiagnosis>()
                .HasKey(sd => new { sd.SymptomsId, sd.DiagnosisId });

            modelBuilder.Entity<SymptomDiagnosis>()
                .HasOne(sd => sd.Symptoms)
                .WithMany()
                .HasForeignKey(sd => sd.SymptomsId);

            modelBuilder.Entity<SymptomDiagnosis>()
                .HasOne(sd => sd.Diagnosis)
                .WithMany()
                .HasForeignKey(sd => sd.DiagnosisId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<HealthItem>()
                .HasKey(h => h.Id);

            //Configuration for constraints
            modelBuilder.Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(p => p.DOB)
                .IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .IsRequired();

            modelBuilder.Entity<Encounter>()
                .Property(e => e.EncounterDate)
                .IsRequired();

            modelBuilder.Entity<Symptoms>()
                .Property(s => s.Description)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Diagnosis>()
                .Property(d => d.DiagnosisResult)
                .HasMaxLength(500)
                .IsRequired();
            modelBuilder.Entity<Diagnosis>()
                .Property(d => d.DiagnosisDate)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.FirstName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
