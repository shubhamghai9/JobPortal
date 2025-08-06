using JobPortal.API.Dtos;
using JobPortal.API.Dtos.Pagination;
using MediatR;

namespace JobPortal.API.Features.Jobs.Queries.GetJobs
{
    public class GetJobsQuery : IRequest<PagedResponse<JobDto>>
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
