using DataAccess.DataModel;

namespace DataAccess.Repositories;

public interface INoteRepository
{
    public Task<DbNote> CreateNoteAsync(DbNote note);
    public Task<DbNote?> GetNoteAsync(Guid id);
    public Task<IReadOnlyList<DbNote>> GetNotesAsync(int skip, int take);
}
