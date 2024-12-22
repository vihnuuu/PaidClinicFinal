namespace DAL.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public QualificationLevel Qualifications { get; set; }
        public string Email { get; set; }
    }

    public enum QualificationLevel
    {
        MD,
        PhD,
        DO,
        MBBS,
        Specialist
    }
}
