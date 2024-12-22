using System;
using System.Collections.Generic;
using DAL.Entities.Enums;

namespace DAL.Entities
{
    public class Visit
    {
        public int VisitId { get; set; }
        public int AdminId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public VisitStatus Status { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public decimal TotalCost { get; set; }
        public ICollection<Treatment> Treatments { get; set; }

        // Зв'язок із Doctor
        public Doctor Doctor { get; set; }

        // Зв'язок із Patient
        public Patient Patient { get; set; }

    }

    public enum VisitStatus
    {
        NotStarted,
        Active,
        Completed
    }
}
