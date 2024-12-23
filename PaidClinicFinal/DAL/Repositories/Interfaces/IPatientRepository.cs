using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
     public interface IPatientRepository
    {
        // Знайти пацієнта за електронною поштою
        Task<Patient> FindByEmailAsync(string email);

        // Отримати всі візити пацієнта
        Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(int patientId);

        // Отримати всі візити пацієнта за певний період
        Task<IEnumerable<Visit>> GetVisitsByPatientIdAndPeriodAsync(int patientId, DateTime startDate, DateTime endDate);
    }
}
