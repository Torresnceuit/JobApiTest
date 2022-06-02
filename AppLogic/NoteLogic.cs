using DataAccess.DataModel;
using DataAccess.Repositories;
using ServiceModel;

namespace AppLogic;

public class NoteLogic : INoteLogic
{
    private readonly INoteRepository _noteRepo;
    private readonly IObjectMapper _mapper;

    public NoteLogic(INoteRepository noteRepo, IObjectMapper mapper)
    {
        _noteRepo = noteRepo ?? throw new ArgumentNullException(nameof(noteRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        if (note is null)
        {
            throw new ArgumentNullException(nameof(note));
        }

        var dbObject = _mapper.Map<Note, DbNote>(note);
        var result = await _noteRepo.CreateNoteAsync(dbObject).ConfigureAwait(false);
        return _mapper.Map<DbNote, Note>(result);
    }

    public async Task<Note> GetNoteAsync(Guid id)
    {
        var result = await _noteRepo.GetNoteAsync(id).ConfigureAwait(false);
        return _mapper.Map<DbNote, Note>(result);
    }

    public async Task<IEnumerable<Note>> GetNotesAsync(int skip = 0, int take = 100)
    {
        var result = await _noteRepo.GetNotesAsync(skip, take).ConfigureAwait(false);
        return _mapper!.Map<DbNote, Note>(result);
    }
}
