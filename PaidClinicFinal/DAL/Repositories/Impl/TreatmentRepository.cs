using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class TreatmentRepository : BaseRepository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(DbContext context) : base(context)
        {
        }

        // Отримати всі лікування для візиту
        public async Task<IEnumerable<Treatment>> GetTreatmentsByVisitIdAsync(int visitId)
        {
            return await _set.Where(t => t.VisitId == visitId).ToListAsync();
        }

        // Отримати лікування з вартістю більше певної суми
        public async Task<IEnumerable<Treatment>> GetExpensiveTreatmentsAsync(decimal minCost)
        {
            return await _set.Where(t => t.Cost > minCost).ToListAsync();
        }
    }
}
