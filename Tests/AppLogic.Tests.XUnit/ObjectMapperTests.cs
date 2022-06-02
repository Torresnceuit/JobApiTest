using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using ServiceModel;
using Shouldly;
using Xunit;

namespace AppLogic.Tests.XUnit;

public class ObjectMapperTests
{
    private readonly IObjectMapper _mapper = new ObjectMapper();

    [Fact]
    public void Map_unknown_throws() => Should.Throw<NotSupportedException>(() => _mapper.Map<int, string>(2));

    [Fact]
    public void Map_nullObject_returnsNull_doesNotThrow()
    {
        var result = Should.NotThrow(() => _mapper.Map<Job, DbJob>((Job)null!));
        result.ShouldBeNull();
    }

    [Fact]
    public void Map_nullCollection_returnsNull_doesNotThrow()
    {
        var result = Should.NotThrow(() => _mapper.Map<Job, DbJob>((IEnumerable<Job>)null!));
        result.ShouldBeNull();
    }

    [Fact]
    public void Map_Job_ReturnsCorrectlyMappedDbJob()
    {
        var source = new Job(Guid.NewGuid(), JobStatus.ACTIVE, Guid.NewGuid());
        var result = _mapper.Map<Job, DbJob>(source);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<DbJob>();
        result.Id.ShouldBe(source.Id);
        result.ClientId.ShouldBe(source.ClientId);
    }

    [Fact]
    public void Map_JobCollection_ReturnsMappedDbJobCollection()
    {
        var values = new List<Job>
            {
                new (Guid.NewGuid(), JobStatus.ACTIVE, Guid.NewGuid()),
                new (Guid.NewGuid(), JobStatus.COMPLETED, Guid.NewGuid()),
            };

        var result = _mapper.Map<Job, DbJob>(values);

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(2);
        result.ShouldBeAssignableTo<IEnumerable<DbJob>>();
    }

    [Fact]
    public void Map_DbJob_ReturnsCorrectlyMappedJob()
    {
        var source = new DbJob(Guid.NewGuid(), JobStatus.ACTIVE, Guid.NewGuid());
        var result = _mapper.Map<DbJob, Job>(source);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Job>();
        result.Id.ShouldBe(source.Id);
        result.ClientId.ShouldBe(source.ClientId);
    }

    [Fact]
    public void Map_DbJobCollection_ReturnsMappedJobCollection()
    {
        var values = new List<DbJob>
            {
                new (Guid.NewGuid(), JobStatus.ACTIVE, Guid.NewGuid()),
                new (Guid.NewGuid(), JobStatus.COMPLETED, Guid.NewGuid()),
            };

        var result = _mapper.Map<DbJob, Job>(values);

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(2);
        result.ShouldBeAssignableTo<IEnumerable<Job>>();
    }

    [Fact]
    public void Map_Client_ReturnsCorrectlyMappedDbClient()
    {
        var source = new Client(Guid.NewGuid(), "Client-A", "A@client.com", "0123456789");
        var result = _mapper.Map<Client, DbClient>(source);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<DbClient>();
        result.Id.ShouldBe(source.Id);
        result.Name.ShouldBe(source.Name);
    }

    [Fact]
    public void Map_ClientCollection_ReturnsMappedDbClientCollection()
    {
        var values = new List<Client>
            {
                new Client(Guid.NewGuid(), "Client-A", "A@client.com", "01234567891"),
                new Client(Guid.NewGuid(), "Client-B", "B@client.com", "01234567890"),
            };

        var result = _mapper.Map<Client, DbClient>(values);

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(2);
        result.ShouldBeAssignableTo<IEnumerable<DbClient>>();
    }

    [Fact]
    public void Map_DbClient_ReturnsCorrectlyMappedClient()
    {
        var source = new DbClient(Guid.NewGuid(), "Client-A", "A@client.com", "0123456789");
        var result = _mapper.Map<DbClient, Client>(source);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Client>();
        result.Id.ShouldBe(source.Id);
        result.Name.ShouldBe(source.Name);
    }

    [Fact]
    public void Map_DbClientCollection_ReturnsMappedClientCollection()
    {
        var values = new List<DbClient>
            {
                new DbClient(Guid.NewGuid(), "Client-A", "A@client.com", "01234567891"),
                new DbClient(Guid.NewGuid(), "Client-B", "B@client.com", "01234567890"),
            };

        var result = _mapper.Map<DbClient, Client>(values);

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(2);
        result.ShouldBeAssignableTo<IEnumerable<Client>>();
    }

    [Fact]
    public void Map_Note_ReturnsCorrectlyMappedDbNote()
    {
        var source = new Note(Guid.NewGuid(), "new content", Guid.NewGuid());
        var result = _mapper.Map<Note, DbNote>(source);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<DbNote>();
        result.Id.ShouldBe(source.Id);
        result.JobId.ShouldBe(source.JobId);
    }

    [Fact]
    public void Map_NoteCollection_ReturnsMappedDbNoteCollection()
    {
        var values = new List<Note>
            {
                new Note(Guid.NewGuid(), "new content A", Guid.NewGuid()),
                new Note(Guid.NewGuid(), "new content B", Guid.NewGuid()),
            };

        var result = _mapper.Map<Note, DbNote>(values);

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(2);
        result.ShouldBeAssignableTo<IEnumerable<DbNote>>();
    }

    [Fact]
    public void Map_DbNote_ReturnsCorrectlyMappedNote()
    {
        var source = new DbNote(Guid.NewGuid(), "new content", Guid.NewGuid());
        var result = _mapper.Map<DbNote, Note>(source);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Note>();
        result.Id.ShouldBe(source.Id);
        result.JobId.ShouldBe(source.JobId);
    }

    [Fact]
    public void Map_DbNoteCollection_ReturnsMappedNoteCollection()
    {
        var values = new List<DbNote>
            {
                new DbNote(Guid.NewGuid(), "new content A", Guid.NewGuid()),
                new DbNote(Guid.NewGuid(), "new content B", Guid.NewGuid()),
            };

        var result = _mapper.Map<DbNote, Note>(values);

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(2);
        result.ShouldBeAssignableTo<IEnumerable<Note>>();
    }
}
