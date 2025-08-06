using AutoMapper;
using JobPortal.API.Repositories.Interfaces;
using MediatR;

namespace JobPortal.API.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, bool>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public UpdateJobCommandHandler(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetByIdAsync(request.Id);
            if (job == null) return false;

            _mapper.Map(request.JobDto, job);
            await _repository.UpdateAsync(job);
            return true;
        }
    }
}
