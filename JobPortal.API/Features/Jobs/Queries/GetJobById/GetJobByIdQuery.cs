using JobPortal.API.Dtos;
using MediatR;

namespace JobPortal.API.Features.Jobs.Queries.GetJobById
{
    public record GetJobByIdQuery(int Id) : IRequest<JobDto>;
}
