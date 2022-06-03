using AppLogic;
using DataAccess.DataModel;
using Microsoft.AspNetCore.Mvc;
using ServiceModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobLogic _jobLogic;

    public JobController(IJobLogic jobLogic)
    {
        _jobLogic = jobLogic;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Job job)
    {
        await _jobLogic.CreateJobAsync(job).ConfigureAwait(false);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip = 0, int take = 100)
    {
        var result = await _jobLogic.GetJobsAsync(skip, take).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(JobStatus status, Guid clientId, string sort = "Time", string direction = "desc", int take = 100)
    {
        var result = await _jobLogic.GetJobsAsync(status, clientId, sort, direction, take).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _jobLogic.GetJobAsync(id).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    [Route("{jobId:Guid}/notes")]
    public async Task<IActionResult> GetJobs(Guid jobId)
    {
        var result = await _jobLogic.GetNotesAsync(jobId).ConfigureAwait(false);
        return Ok(result);
    }
}
