namespace JobPortal.API.Dtos
{
    public class JobFilterDto
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
