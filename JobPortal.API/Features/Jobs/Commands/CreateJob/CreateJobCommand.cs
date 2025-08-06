using JobPortal.API.Dtos;
using MediatR;

namespace JobPortal.API.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<JobDto>
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
