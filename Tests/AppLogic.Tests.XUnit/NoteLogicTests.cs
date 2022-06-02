
using DataAccess.Repositories;
using Moq;
using Shouldly;
using Xunit;
using ServiceModel;
using DataAccess.DataModel;

namespace AppLogic.Tests.XUnit;

public class NoteLogicTests
{
    [Fact]
    public void ConstructorThrowsArgumentNullException_When_LegalRepoNull()
    {
        // arrange
        var mapper = Mock.Of<IObjectMapper>();

        // act
        var action = () => new NoteLogic(noteRepo: null!, mapper);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorThrowsArgumentNullException_When_MapperNull()
    {
        // arrange
        var noteRepo = Mock.Of<INoteRepository>();

        // act
        var action = () => new NoteLogic(noteRepo, mapper: null!);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void CreateNoteThrowsArgumentNullException_When_NoteNull()
    {
        // arrange
        var noteRepository = Mock.Of<INoteRepository>();
        var mapper = Mock.Of<IObjectMapper>();
        var notelLogic = new NoteLogic(noteRepository, mapper);

        // act
        var action = () => notelLogic.CreateNoteAsync(null!);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateNoteReturnUpdatedNote_When_IdIsExisting()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var clientId = Guid.NewGuid();
        context.Client.Add(new
            (
                clientId,
                "Client-A",
                "ClientA@gmail.com",
                "01234567890"
            ));

        Guid jobId = Guid.NewGuid();
        context.Job.Add(new
        (
            jobId,
            JobStatus.ACTIVE,
            clientId
        ));

        Guid noteId = Guid.NewGuid();
        context.Note.Add(new
        (
            noteId,
            "Content-A",
            jobId
        ));

        context.Note.Add(new
        (
            Guid.NewGuid(),
            "Content-B",
            jobId
        ));

        await context.SaveChangesAsync();

        var noteRepository = new NoteRepository(context);
        var noteLogic = new NoteLogic(noteRepository, mapper);
        var note = new Note(noteId, "Content-1", jobId);

        // act
        var action = await noteLogic.CreateNoteAsync(note);

        // assert
        Assert.NotNull(action);
        Assert.Equal(note.Id, action.Id);
        Assert.Equal(note.Content, action.Content);
        Assert.Equal(note.JobId, action.JobId);
    }

    [Fact]
    public async Task CreateNoteReturnNull_When_JobIdIsInValid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        await context.SaveChangesAsync();

        var noteRepository = new NoteRepository(context);
        var noteLogic = new NoteLogic(noteRepository, mapper);
        var note = new Note(Guid.NewGuid(), "Content-1", Guid.NewGuid());

        // act
        var action = await noteLogic.CreateNoteAsync(note);

        // assert
        Assert.Null(action);
    }

    [Fact]
    public async Task CreateNoteReturnNote_When_JobIdIsValid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var clientId = Guid.NewGuid();
        context.Client.Add(new
            (
                clientId,
                "Client-A",
                "ClientA@gmail.com",
                "01234567890"
            ));

        Guid jobId = Guid.NewGuid();
        context.Job.Add(new
        (
            jobId,
            JobStatus.ACTIVE,
            clientId
        ));

        Guid noteId = Guid.NewGuid();
        context.Note.Add(new
        (
            noteId,
            "Content-A",
            jobId
        ));

        context.Note.Add(new
        (
            Guid.NewGuid(),
            "Content-B",
            jobId
        ));

        await context.SaveChangesAsync();

        var noteRepository = new NoteRepository(context);
        var noteLogic = new NoteLogic(noteRepository, mapper);
        var note = new Note(Guid.NewGuid(), "Content-1", jobId);

        // act
        var action = await noteLogic.CreateNoteAsync(note);

        // assert
        action.ShouldBeAssignableTo<Note>();
        Assert.Equal(note.Id, action.Id);
        Assert.Equal(note.Content, action.Content);
        Assert.Equal(note.JobId, action.JobId);
    }

    [Fact]
    public async Task GetNoteReturnNull_When_MatterIdIsInValid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var clientId = Guid.NewGuid();
        context.Client.Add(new
            (
                clientId,
                "Client-A",
                "ClientA@gmail.com",
                "01234567890"
            ));

        Guid jobId = Guid.NewGuid();
        context.Job.Add(new
        (
            jobId,
            JobStatus.ACTIVE,
            clientId
        ));

        Guid noteId = Guid.NewGuid();
        context.Note.Add(new
        (
            noteId,
            "Content-A",
            jobId
        ));

        context.Note.Add(new
        (
            Guid.NewGuid(),
            "Content-B",
            jobId
        ));

        await context.SaveChangesAsync();

        var noteRepository = new NoteRepository(context);
        var noteLogic = new NoteLogic(noteRepository, mapper);

        // act
        var action = await noteLogic.GetNoteAsync(Guid.NewGuid());

        // assert
        Assert.Null(action);
    }

    [Fact]
    public async Task GetNoteReturnNote_When_JobIdIsValid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var clientId = Guid.NewGuid();
        context.Client.Add(new
            (
                clientId,
                "Client-A",
                "ClientA@gmail.com",
                "01234567890"
            ));

        Guid jobId = Guid.NewGuid();
        context.Job.Add(new
        (
            jobId,
            JobStatus.ACTIVE,
            clientId
        ));

        Guid noteId = Guid.NewGuid();
        context.Note.Add(new
        (
            noteId,
            "Content-A",
            jobId
        ));

        context.Note.Add(new
        (
            Guid.NewGuid(),
            "Content-B",
            jobId
        ));

        await context.SaveChangesAsync();

        var noteRepository = new NoteRepository(context);
        var noteLogic = new NoteLogic(noteRepository, mapper);

        // act
        var action = await noteLogic.GetNoteAsync(noteId);

        // assert
        action.ShouldBeAssignableTo<Note>();
        Assert.Equal(noteId, action.Id);
        Assert.Equal("Content-A", action.Content);
        Assert.Equal(jobId, action.JobId);
    }

    [Fact]
    public async Task GetNotesReturnListOfNotes()
    {
        // arrange
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var clientId = Guid.NewGuid();
        context.Client.Add(new
            (
                clientId,
                "Client-A",
                "ClientA@gmail.com",
                "01234567890"
            ));

        Guid jobId = Guid.NewGuid();
        context.Job.Add(new
        (
            jobId,
            JobStatus.ACTIVE,
            clientId
        ));

        Guid noteId = Guid.NewGuid();
        context.Note.Add(new
        (
            noteId,
            "Content-A",
            jobId
        ));

        context.Note.Add(new
        (
            Guid.NewGuid(),
            "Content-B",
            jobId
        ));

        await context.SaveChangesAsync();

        var mapper = new ObjectMapper();
        var noteRepository = new NoteRepository(context);
        var noteLogic = new NoteLogic(noteRepository, mapper);

        // act
        var action = await noteLogic.GetNotesAsync();

        // assert
        action.ShouldBeAssignableTo<IEnumerable<Note>>();
        Assert.Equal(2, action.Count());
    }
}
