using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public  interface ITreatmentRepository
    {
        // Отримати всі лікування для візиту
        Task<IEnumerable<Treatment>> GetTreatmentsByVisitIdAsync(int visitId);

        // Отримати лікування з вартістю більше певної суми
        Task<IEnumerable<Treatment>> GetExpensiveTreatmentsAsync(decimal minCost);
    }
}
