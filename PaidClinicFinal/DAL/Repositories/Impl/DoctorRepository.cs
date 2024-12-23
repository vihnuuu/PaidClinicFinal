using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DbContext context) : base(context)
        {
        }

        // Отримати всіх лікарів за спеціалізацією
        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(string specialty)
        {
            return await _set.Where(d => d.Specialty == specialty).ToListAsync();
        }

        // Отримати всі візити лікаря
        public async Task<IEnumerable<Visit>> GetVisitsByDoctorIdAsync(int doctorId)
        {
            return await _context.Set<Visit>()
                .Where(v => v.DoctorId == doctorId)
                .Include(v => v.Patient)
                .ToListAsync();
        }

        // Отримати детальну історію візитів лікаря(з лікуваннями)
        public async Task<IEnumerable<Visit>> GetDetailedVisitsByDoctorIdAsync(int doctorId)
        {
            return await _context.Set<Visit>()
                .Where(v => v.DoctorId == doctorId)
                .Include(v => v.Patient)     // Завантажити інфо про пацієнтів
                .Include(v => v.Treatments) // Завантажити інфо про лікування
                .ToListAsync();
        }

        // Отримати візити лікаря за певний період
        public async Task<IEnumerable<Visit>> GetVisitsByDoctorIdAndPeriodAsync(int doctorId, DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Visit>()
                .Where(v => v.DoctorId == doctorId && v.VisitDate >= startDate && v.VisitDate <= endDate)
                .Include(v => v.Patient)     // Завантажити інфо про пацієнтів
                .Include(v => v.Treatments) // Завантажити інфо про лікування
                .ToListAsync();
        }

        // Отримати кількість пацієнтів, яких обслуговував лікар
        public async Task<int> GetPatientCountByDoctorIdAsync(int doctorId)
        {
            return await _context.Set<Visit>()
                .Where(v => v.DoctorId == doctorId)
                .Select(v => v.PatientId) // Вибрати унікальні айді пацієнтів
                .Distinct()              // Уникнути повторень
                .CountAsync();
        }
    }
}
