using AppLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteLogic _noteLogic;

    public NoteController(INoteLogic noteLogic)
    {
        _noteLogic = noteLogic;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip = 0, int take = 100)
    {
        var result = await _noteLogic.GetNotesAsync(skip, take).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _noteLogic.GetNoteAsync(id).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Note note)
    {
        await _noteLogic.CreateNoteAsync(note).ConfigureAwait(false);
        return Ok();
    }
}
