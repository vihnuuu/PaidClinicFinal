using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository Patients { get; }
        IDoctorRepository Doctors { get; }
        IVisitRepository Visits { get; }
        ITreatmentRepository Treatments { get; }
        void Save();
    }
}
