using AutoMapper;
using JobPortal.API.Domain.Entities;
using JobPortal.API.Dtos;
using JobPortal.API.Repositories.Interfaces;
using MediatR;

namespace JobPortal.API.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobDto>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public CreateJobCommandHandler(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _mapper.Map<Job>(request);
            job.PostedDate = DateTime.UtcNow;

            var savedJob = await _repository.AddAsync(job);
            return _mapper.Map<JobDto>(savedJob);
        }
    }
}
