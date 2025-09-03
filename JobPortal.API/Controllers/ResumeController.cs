using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{

    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/resume")]
    [Authorize]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeTextExtractor _extractor;
        private readonly IAiResumeAnalyzer _analyzer;

        public ResumeController(IResumeTextExtractor extractor, IAiResumeAnalyzer analyzer)
        {
            _extractor = extractor;
            _analyzer = analyzer;
        }

        [Authorize(Roles = "Admin,Recruiter")]
        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            var text = await _extractor.ExtractTextAsync(file, ct);
            var analysis = await _analyzer.AnalyzeResumeAsync(text, ct);

            return Ok(new { extractedText = text, aiAnalysis = analysis });
        }
    }
}
