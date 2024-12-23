using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        // Отримати всіх лікарів за спеціалізацією
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(string specialty);

        // Отримати всі візити лікаря
        Task<IEnumerable<Visit>> GetVisitsByDoctorIdAsync(int doctorId);

        // Отримати детальну історію візитів лікаря (з лікуваннями)
        Task<IEnumerable<Visit>> GetDetailedVisitsByDoctorIdAsync(int doctorId);

        // Отримати візити лікаря за певний період
        Task<IEnumerable<Visit>> GetVisitsByDoctorIdAndPeriodAsync(int doctorId, DateTime startDate, DateTime endDate);

        // Отримати кількість пацієнтів, яких обслуговував лікар
        Task<int> GetPatientCountByDoctorIdAsync(int doctorId);
    }
}

