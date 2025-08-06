namespace JobPortal.API.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PostedDate { get; set; }
    }
}
