using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly TechTestDbContext _techTestDbContext;

    public NoteRepository(TechTestDbContext techTestDbContext)
    {
        _techTestDbContext = techTestDbContext;
    }

    public async Task<DbNote?> CreateNoteAsync(DbNote newNote)
    {
        if (!_techTestDbContext.Job.Select(x => x.Id).Contains(newNote.JobId))
        {
            return null;
        }

        var exist = await _techTestDbContext.Note.FirstOrDefaultAsync(x => x.Id == newNote.Id);
        if (exist == null)
        {
            exist = new DbNote();
            exist.Id = newNote.Id;
            await _techTestDbContext.AddAsync(newNote);
        }

        exist.Update(newNote);
        await _techTestDbContext.SaveChangesAsync();
        return exist;
    }

    public async Task<IReadOnlyList<DbNote>> GetNotesAsync(int skip = 0, int take = 100) => await _techTestDbContext.Note
        .Skip(skip)
        .Take(take)
        .ToListAsync()
        .ConfigureAwait(continueOnCapturedContext: false);

    public Task<DbNote?> GetNoteAsync(Guid id) => _techTestDbContext.Note.FirstOrDefaultAsync(x => x.Id == id);
}
