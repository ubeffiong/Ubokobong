using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IHVNMedix.Data;
using IHVNMedix.Services;
using IHVNMedix.Repositories;

namespace IHVNMedix
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the DbContext and specify the connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IEncounterRepository, EncounterRepository>();
            services.AddScoped<ISymptomsRepository, SymptomsRepository>();
            services.AddScoped<IVitalSignsRepository, VitalSignsRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            //registering the MedicalDiagnosisService as a singleton
            services.AddHttpClient<MedicalDiagnosisService>(client =>
            {
                client.BaseAddress = new Uri(_configuration["HealthServiceUrl"]);
            });
            // Registering IConfiguration for access in services
            services.AddScoped<IMedicalDiagnosisService, MedicalDiagnosisService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //Patient Controller route
                endpoints.MapControllerRoute(
                    name: "patient",
                    pattern: "Patient/{action=Index}/{id}",
                    defaults: new { controller = "Patient" });

                //Encounter route
                endpoints.MapControllerRoute(
                    name: "encounter",
                    pattern: "Encounter/{action=Index}/{id}",
                    defaults: new { controller = "Encounter" });

                //diagnosis route
                endpoints.MapControllerRoute(
                    name: "diagnosis",
                    pattern: "Diagnosis/{action=Index}/{id}",
                    defaults: new { controller = "Diagnosis" });

                //doctor route
                endpoints.MapControllerRoute(
                    name: "doctor",
                    pattern: "Doctor/{action=Index}/{id}",
                    defaults: new { controller = "Doctor" });
            });
        }
    }
}
