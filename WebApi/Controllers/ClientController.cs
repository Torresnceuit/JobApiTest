using AppLogic;
using Microsoft.AspNetCore.Mvc;
using ServiceModel;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientLogic _clientLogic;

    public ClientController(IClientLogic clientLogic)
    {
        _clientLogic = clientLogic;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client client)
    {
        await _clientLogic.CreateClientAsync(client).ConfigureAwait(false);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip = 0, int take = 100)
    {
        var result = await _clientLogic.GetClientsAsync(skip, take).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _clientLogic.GetClientAsync(id).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    [Route("{clientId:Guid}/jobs")]
    public async Task<IActionResult> GetJobs(Guid clientId)
    {
        var result = await _clientLogic.GetJobsAsync(clientId).ConfigureAwait(false);
        return Ok(result);
    }
}
