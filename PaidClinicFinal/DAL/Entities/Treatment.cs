namespace DAL.Entities
{
    public class Treatment
    {
        public int TreatmentId { get; set; }
        public int VisitId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public Visit Visit { get; set; }
    }
}
