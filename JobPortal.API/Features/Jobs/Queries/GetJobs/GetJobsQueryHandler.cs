using AutoMapper;
using JobPortal.API.Dtos;
using JobPortal.API.Dtos.Pagination;
using JobPortal.API.Repositories.Interfaces;
using MediatR;

namespace JobPortal.API.Features.Jobs.Queries.GetJobs
{
    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, PagedResponse<JobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            var (jobs, totalCount) = await _jobRepository.GetFilteredJobsAsync(
                request.Title, request.Location, request.PageNumber, request.PageSize);

            var jobDtos = _mapper.Map<List<JobDto>>(jobs);

            return new PagedResponse<JobDto>(jobDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}
