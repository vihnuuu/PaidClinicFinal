using DAL.Repositories.Interfaces;
using DAL.Repositories.Impl;
using System;

namespace DAL.EF
{
    public class EFUnitOfWork : IDisposable
    {
        // Контекст бази даних, який використовується для роботи з таблицями
        private readonly ClinicContext _context;

        private PatientRepository _patientRepository;
        private DoctorRepository _doctorRepository;
        private VisitRepository _visitRepository;
        private TreatmentRepository _treatmentRepository;

        // Конструктор класу, приймає об'єкт контексту бази даних
        public EFUnitOfWork(ClinicContext context)
        {
            _context = context; // Зберігаємо контекст для подальшої роботи
        }

        // Властивість для доступу до репозиторію пацієнтів
        public IPatientRepository Patients
        {
            get
            {
                // Якщо репозиторій ще не створений, створюємо його
                if (_patientRepository == null)
                {
                    _patientRepository = new PatientRepository(_context);
                }
                return _patientRepository; // Повертаємо екземпляр репозиторію
            }
        }


        public IDoctorRepository Doctors
        {
            get
            {
                if (_doctorRepository == null)
                {
                    _doctorRepository = new DoctorRepository(_context);
                }
                return _doctorRepository;
            }
        }

        public IVisitRepository Visits
        {
            get
            {
                if (_visitRepository == null)
                {
                    _visitRepository = new VisitRepository(_context);
                }
                return _visitRepository;
            }
        }

        public ITreatmentRepository Treatments
        {
            get
            {
                if (_treatmentRepository == null)
                {
                    _treatmentRepository = new TreatmentRepository(_context);
                }
                return _treatmentRepository;
            }
        }

        // Метод для збереження всіх змін у базі даних
        public void Save()
        {
            // Викликаємо метод SaveChanges контексту для фіксації змін
            _context.SaveChanges();
        }

        // Поле для визначення, чи об'єкт вже звільнено
        private bool _disposed = false;

        // Метод для очищення ресурсів
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed) // Якщо ресурси ще не звільнено
            {
                if (disposing) // Звільняємо керовані ресурси
                {
                    _context.Dispose(); // Звільняємо контекст бази даних
                }
                _disposed = true; // Позначаємо об'єкт як звільнений
            }
        }

        // Реалізація інтерфейсу IDisposable
        public void Dispose()
        {
            Dispose(true); // Викликаємо метод Dispose з параметром true
            GC.SuppressFinalize(this); // Забороняємо фіналізацію об'єкта
        }
    }
}
