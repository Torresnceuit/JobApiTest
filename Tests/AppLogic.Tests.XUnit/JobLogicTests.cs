using DataAccess.DataModel;
using DataAccess.Repositories;
using Moq;
using ServiceModel;
using Shouldly;
using Xunit;

namespace AppLogic.Tests.XUnit;
public class JoLogicTests
{
    [Fact]
    public void ConstructorThrowsArgumentNullException_When_ClientRepoNull()
    {
        // arrange
        var mapper = Mock.Of<IObjectMapper>();

        // act
        var action = () => new JobLogic(jobRepo: null!, mapper);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorThrowsArgumentNullException_When_MapperNull()
    {
        // arrange
        var jobRepo = Mock.Of<IJobRepository>();

        // act
        var action = () => new JobLogic(jobRepo, mapper: null!);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void CreateJobThrowsArgumentNullException_When_JobNull()
    {
        // arrange
        var jobRepository = Mock.Of<IJobRepository>();
        var mapper = Mock.Of<IObjectMapper>();
        var jobLogic = new JobLogic(jobRepository, mapper);

        // act
        var action = () => jobLogic.CreateJobAsync(null!);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    public async Task CreateJobReturnNull_When_ClientIdIsInValid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);
        var newJob = new Job(Guid.NewGuid(), JobStatus.INVOICING, Guid.NewGuid());

        // act
        var action = await jobLogic.CreateJobAsync(newJob);

        // assert
        Assert.Null(action);
    }

    [Fact]
    public async Task CreateJobReturnAJob_When_ClientIdIsValid()
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

        await context.SaveChangesAsync();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);
        var newJob = new Job(Guid.NewGuid(), JobStatus.INVOICING, clientId);

        // act
        var action = await jobLogic.CreateJobAsync(newJob);

        // assert
        action.ShouldBeAssignableTo<Job>();
        Assert.Equal(newJob.Id, action.Id);
        Assert.Equal(newJob.Status, action.Status);
        Assert.Equal(newJob.ClientId, action.ClientId);
    }

    [Fact]
    public async Task CreateJobReturnUpdatedJob_When_IdExisting()
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

        var jobId = Guid.NewGuid();
        context.Job.Add(new
            (
                jobId,
                JobStatus.COMPLETED,
                Guid.NewGuid()
            ));

        await context.SaveChangesAsync();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);
        var newJob = new Job(jobId, JobStatus.TOPRICED, clientId);

        // act
        var action = await jobLogic.CreateJobAsync(newJob);

        // assert
        action.ShouldNotBeNull();
        action.ShouldBeAssignableTo<Job>();
        Assert.Equal(newJob.Id, action.Id);
        Assert.Equal(newJob.Status, action.Status);
        Assert.Equal(newJob.ClientId, action.ClientId);
    }

    [Fact]
    public async Task GetJobReturnNull_When_JobIdIsInvalid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var jobId = Guid.NewGuid();
        context.Job.Add(new
            (
                jobId,
                JobStatus.COMPLETED,
                Guid.NewGuid()
            ));

        await context.SaveChangesAsync();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);

        // act
        var action = await jobLogic.GetJobAsync(Guid.NewGuid());

        // assert
        Assert.Null(action);
    }

    [Fact]
    public async Task GetJobsReturnAllJobs()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();


        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.COMPLETED,
                Guid.NewGuid()
            ));

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.INVOICING,
                Guid.NewGuid()
            ));

        await context.SaveChangesAsync();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);

        // act
        var action = await jobLogic.GetJobsAsync();

        // assert
        Assert.NotNull(action);
        Assert.Equal(2, action.Count());
    }

    [Fact]
    public async Task FilterJobsReturnAllFilteredJobs()
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

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.COMPLETED,
                clientId
            ));

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.INVOICING,
                clientId
            ));

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.COMPLETED,
                clientId
            ));

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.COMPLETED,
                clientId
            ));

        await context.SaveChangesAsync();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);

        // act
        var action = await jobLogic.GetJobsAsync(JobStatus.COMPLETED, clientId);

        // assert
        Assert.NotNull(action);
        Assert.Equal(3, action.Count());
    }

    [Fact]
    public async Task GetAllNotesOfJobReturnAllNotes_When_JobIdIsValid()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var jobId = Guid.NewGuid();
        context.Job.Add(new
            (
                jobId,
                JobStatus.COMPLETED,
                Guid.NewGuid()
            ));

        await context.SaveChangesAsync();

        context.Note.Add(new
            (
                Guid.NewGuid(),
                "Content-A",
                jobId
            ));

        context.Note.Add(new
            (
                Guid.NewGuid(),
                "Content-B",
                jobId
            ));

        context.Note.Add(new
            (
                Guid.NewGuid(),
                "Content-C",
                jobId
            ));

        await context.SaveChangesAsync();

        var jobRepository = new JobRepository(context);
        var jobLogic = new JobLogic(jobRepository, mapper);

        // act
        var action = await jobLogic.GetNotesAsync(jobId);

        // assert
        Assert.NotNull(action);
        Assert.Equal(3, action.Count());
    }
}
