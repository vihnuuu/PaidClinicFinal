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
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DbContext context) : base(context)
        {
        }

        // Знайти пацієнта за електронною поштою
        public async Task<Patient> FindByEmailAsync(string email)
        {
            return await _set.FirstOrDefaultAsync(p => p.Email == email);
        }

        // Отримати всі візити пацієнта
        public async Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(int patientId)
        {
            return await _context.Set<Visit>()
                .Where(v => v.PatientId == patientId)
                .Include(v => v.Doctor)
                .Include(v => v.Treatments)
                .ToListAsync();
        }

        // Отримати всі візити пацієнта за певний період
        public async Task<IEnumerable<Visit>> GetVisitsByPatientIdAndPeriodAsync(int patientId, DateTime startDate, DateTime endDate)
        {
            // Завантажуємо візити пацієнта, що відповідають заданому періоду
            // Додаємо зв'язки з лікарями та лікуваннями
            return await _context.Set<Visit>()
                .Where(v => v.PatientId == patientId && v.VisitDate >= startDate && v.VisitDate <= endDate)
                .Include(v => v.Doctor)
                .Include(v => v.Treatments)
                .ToListAsync();
        }
    }
}