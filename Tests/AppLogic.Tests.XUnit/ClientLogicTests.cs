using DataAccess.DataModel;
using DataAccess.Repositories;
using Moq;
using ServiceModel;
using Shouldly;
using Xunit;

namespace AppLogic.Tests.XUnit;
public class ClientLogicTests
{
    [Fact]
    public void ConstructorThrowsArgumentNullException_When_ClientRepoNull()
    {
        // arrange
        var mapper = Mock.Of<IObjectMapper>();

        // act
        var action = () => new ClientLogic(clientRepo: null!, mapper);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorThrowsArgumentNullException_When_MapperNull()
    {
        // arrange
        var clientRepo = Mock.Of<IClientRepository>();

        // act
        var action = () => new ClientLogic(clientRepo, mapper: null!);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void CreateLawyerThrowsArgumentNullException_When_ClientNull()
    {
        // arrange
        var clientRepository = Mock.Of<IClientRepository>();
        var mapper = Mock.Of<IObjectMapper>();
        var clientLogic = new ClientLogic(clientRepository, mapper);

        // act
        var action = () => clientLogic.CreateClientAsync(null!);

        // assert
        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateClientReturnAClient()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();

        var clientRepository = new ClientRepository(context);
        var clientLogic = new ClientLogic(clientRepository, mapper);
        var newClient = new Client(Guid.NewGuid(), "Client-A", "ClientA@gmail.com", "0123456789");

        // act
        var action = await clientLogic.CreateClientAsync(newClient);

        // assert
        action.ShouldBeAssignableTo<Client>();
        Assert.Equal(newClient.Name, action.Name);
        Assert.Equal(newClient.Email, action.Email);
        Assert.Equal(newClient.Phone, action.Phone);
    }

    [Fact]
    public async Task CreateClientReturnUpdatedClient_When_IdExisting()
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

        var clientRepository = new ClientRepository(context);
        var clientLogic = new ClientLogic(clientRepository, mapper);
        var newClient = new Client(clientId, "Client-B", "ClientB@gmail.com", "0132345643");

        // act
        var action = await clientLogic.CreateClientAsync(newClient);

        // assert
        action.ShouldNotBeNull();
        action.ShouldBeAssignableTo<Client>();
        Assert.Equal(newClient.Name, action.Name);
        Assert.Equal(newClient.Email, action.Email);
        Assert.Equal(newClient.Phone, action.Phone);
    }

    [Fact]
    public async Task GetClientReturnNull_When_ClientIdIsInvalid()
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

        var clientRepository = new ClientRepository(context);
        var clientLogic = new ClientLogic(clientRepository, mapper);

        // act
        var action = await clientLogic.GetClientAsync(Guid.NewGuid());

        // assert
        Assert.Null(action);
    }

    [Fact]
    public async Task GetClientsReturnAllClients()
    {
        // arrange
        var mapper = new ObjectMapper();
        using var factory = new ConnectionFactory();
        using var context = factory.CreateContextForInMemory();


        context.Client.Add(new
            (
                Guid.NewGuid(),
                "Client-A",
                "ClientA@gmail.com",
                "01234567890"
            ));

        context.Client.Add(new
            (
                Guid.NewGuid(),
                "Client-B",
                "ClientB@gmail.com",
                "01234567891"
            ));

        await context.SaveChangesAsync();

        var clientRepository = new ClientRepository(context);
        var clientLogic = new ClientLogic(clientRepository, mapper);

        // act
        var action = await clientLogic.GetClientsAsync();

        // assert
        Assert.NotNull(action);
        Assert.Equal(2, action.Count());
    }

    [Fact]
    public async Task GetAllJobsOfClientReturnAllJobs_When_ClientIdIsValid()
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

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.COMPLETED,
                clientId
            ));

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.ACTIVE,
                clientId
            ));

        context.Job.Add(new
            (
                Guid.NewGuid(),
                JobStatus.SCHEDULED,
                clientId
            ));

        await context.SaveChangesAsync();

        var clientRepository = new ClientRepository(context);
        var clientLogic = new ClientLogic(clientRepository, mapper);

        // act
        var action = await clientLogic.GetJobsAsync(clientId);

        // assert
        Assert.NotNull(action);
        Assert.Equal(3, action.Count());
    }
}
