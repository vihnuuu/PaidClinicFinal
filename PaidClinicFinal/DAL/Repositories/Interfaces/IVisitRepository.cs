using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        // Отримати візити за статусом
        Task<IEnumerable<Visit>> GetVisitsByStatusAsync(VisitStatus status);

        // Отримати детальну інформацію про візит за його ідентифікатором
        Task<Visit> GetVisitDetailsAsync(int visitId);

        // Отримати кількість візитів за певним статусом
        Task<int> GetVisitCountByStatusAsync(VisitStatus status);

        // Отримати візити за статусом та датою
        Task<IEnumerable<Visit>> GetVisitsByStatusAndDateAsync(VisitStatus status, DateTime date);

        // Отримати візити пацієнтів за ID лікаря
        Task<IEnumerable<Visit>> GetVisitsByDoctorIdAsync(int doctorId);

        // Отримати візити пацієнтів з перевіркою доступу
        Task<IEnumerable<Visit>> GetVisitsByDoctorIdWithAccessCheckAsync(int doctorId, int requestingDoctorId);
    }
}
