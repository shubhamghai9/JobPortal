using AutoMapper;
using JobPortal.API.Domain.Entities;
using JobPortal.API.Features.Jobs.Commands.CreateJob;
using JobPortal.API.Dtos;

namespace JobPortal.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Job, JobDto>();
            CreateMap<JobCreateDto, CreateJobCommand>();
            CreateMap<CreateJobCommand, Job>();
            CreateMap<JobUpdateDto, Job>();
        }
    }
}
