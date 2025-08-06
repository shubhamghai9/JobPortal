using AutoMapper;
using JobPortal.API.Dtos;
using JobPortal.API.Features.Jobs.Commands.CreateJob;
using JobPortal.API.Features.Jobs.Commands.DeleteJob;
using JobPortal.API.Features.Jobs.Commands.UpdateJob;
using JobPortal.API.Features.Jobs.Queries.GetJobById;
using JobPortal.API.Features.Jobs.Queries.GetJobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")] 
    [Route("api/v{version:apiVersion}/jobs")]
    [Authorize]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Admin,Recruiter")]
        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery] GetJobsQuery query)
        {
            try
            {
                _logger.LogInformation("GetJobs called");
                var jobs = await _mediator.Send(query);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving jobs: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateDto dto)
        {
            try
            {
                var command = _mapper.Map<CreateJobCommand>(dto);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating job: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Recruiter")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            try
            {
                var job = await _mediator.Send(new GetJobByIdQuery(id));
                return Ok(job);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving job: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] JobUpdateDto dto)
        {
            var result = await _mediator.Send(new UpdateJobCommand(id, dto));
            return result ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try 
            {
                var result = await _mediator.Send(new DeleteJobCommand(id));
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting job: {ex.Message}");
            }
        }
    }
}
