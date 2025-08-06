using JobPortal.API.Repositories.Interfaces;
using MediatR;

namespace JobPortal.API.Features.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IJobRepository _repository;

        public DeleteJobCommandHandler(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetByIdAsync(request.Id);
            if (job == null) return false;

            await _repository.DeleteAsync(job);
            return true;
        }
    }
}
