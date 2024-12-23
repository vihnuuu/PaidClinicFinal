using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }

        // Колекції DbSet для сутностей
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Treatment> Treatments { get; set; }


        // налаштування моделей
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Doctor)
                .WithMany()
                .HasForeignKey(v => v.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Patient)
                .WithMany()
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.Visit)
                .WithMany(v => v.Treatments)
                .HasForeignKey(t => t.VisitId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
