using AutoMapper;
using JobPortal.API.Dtos;
using JobPortal.API.Repositories.Interfaces;
using MediatR;

namespace JobPortal.API.Features.Jobs.Queries.GetJobById
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDto>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public GetJobByIdQueryHandler(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetByIdAsync(request.Id);

            if (job == null)
                throw new KeyNotFoundException($"Job with ID {request.Id} not found.");

            return _mapper.Map<JobDto>(job);
        }
    }
}
