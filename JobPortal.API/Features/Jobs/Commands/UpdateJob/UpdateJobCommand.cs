using JobPortal.API.Dtos;
using MediatR;

namespace JobPortal.API.Features.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public JobUpdateDto JobDto { get; set; }

        public UpdateJobCommand(int id, JobUpdateDto jobDto)
        {
            Id = id;
            JobDto = jobDto;
        }
    }
}
