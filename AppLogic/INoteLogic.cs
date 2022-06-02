namespace AppLogic;

public interface INoteLogic
{
    public Task<Note> CreateNoteAsync(Note note);
    public Task<Note> GetNoteAsync(Guid id);
    Task<IEnumerable<Note>> GetNotesAsync(int skip, int take);
}
