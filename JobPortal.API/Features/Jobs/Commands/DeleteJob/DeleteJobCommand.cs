using MediatR;

namespace JobPortal.API.Features.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteJobCommand(int id)
        {
            Id = id;
        }
    }
}
