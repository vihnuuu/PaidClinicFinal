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
        Task<IEnumerable<Visit>> GetVisitsByStatusAsync(VisitStatus status);
        Task<Visit> GetVisitDetailsAsync(int visitId);
    }
}
