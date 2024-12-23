using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace DAL.Repositories.Impl
{
    public class VisitRepository : BaseRepository<Visit>, IVisitRepository
    {
        public VisitRepository(DbContext context) : base(context)
        {
        }

        // Отримати візити за статусом
        public async Task<IEnumerable<Visit>> GetVisitsByStatusAsync(VisitStatus status)
        {
            return await _set.Where(v => v.Status == status)
                             .Include(v => v.Doctor)
                             .Include(v => v.Patient)
                             .ToListAsync();
        }

        // Отримати детальну інформацію про візит за його ідентифікатором
        public async Task<Visit> GetVisitDetailsAsync(int visitId)
        {
            return await _set.Include(v => v.Doctor)
                             .Include(v => v.Patient)
                             .Include(v => v.Treatments)
                             .FirstOrDefaultAsync(v => v.VisitId == visitId);
        }

        // Отримати кількість візитів за певним статусом
        public async Task<int> GetVisitCountByStatusAsync(VisitStatus status)
        {
            // Повертає кількість візитів, які мають заданий статус
            return await _set.CountAsync(v => v.Status == status);
        }

        // Отримати візити за статусом та датою
        public async Task<IEnumerable<Visit>> GetVisitsByStatusAndDateAsync(VisitStatus status, DateTime date)
        {
            // Завантажує візити, які відповідають заданому статусу та даті
            return await _set.Where(v => v.Status == status && v.VisitDate.Date == date.Date)
                             .Include(v => v.Doctor)
                             .Include(v => v.Patient)
                             .Include(v => v.Treatments)
                             .ToListAsync();
        }

        // Отримати візити пацієнтів за ID лікаря
        public async Task<IEnumerable<Visit>> GetVisitsByDoctorIdAsync(int doctorId)
        {
            // Повертає всі візити, пов'язані з заданим лікарем
            return await _set.Where(v => v.DoctorId == doctorId)
                             .Include(v => v.Patient) // Завантажує інформацію про пацієнтів
                             .Include(v => v.Treatments) // Завантажує інформацію про лікування
                             .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> GetVisitsByDoctorIdWithAccessCheckAsync(int doctorId, int requestingDoctorId)
        {
            // Перевіряємо, чи лікар запитує власні візити
            if (doctorId != requestingDoctorId)
            {
                throw new UnauthorizedAccessException("Access denied: You can only view visits of your patients.");
            }

            // Повертаємо візити, які обслуговував лікар
            return await _set.Where(v => v.DoctorId == doctorId)
                             .Include(v => v.Patient)     // Завантажуємо дані пацієнтів
                             .Include(v => v.Treatments) // Завантажуємо дані лікувань
                             .ToListAsync();
        }
    }
}