using DataAccess.DataModel;
using ServiceModel;

namespace AppLogic;

public class ObjectMapper : IObjectMapper
{
    public TDest Map<TSource, TDest>(TSource item)
    {
        object? result = item switch
        {
            null => null,
            DbJob job => Map(job),
            Job job => Map(job),
            DbClient client => Map(client),
            Client client => Map(client),
            DbNote note => Map(note),
            Note note => Map(note),
            _ => throw new NotSupportedException()
        };

        return (TDest)result!;
    }

    public IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> sourceCollection) => sourceCollection?.Select(Map<TSource, TDest>);
    private DbJob Map(Job job) => new(job.Id, job.Status, job.ClientId);
    private Job Map(DbJob job) => new(job.Id, job.Status, job.ClientId);
    private DbClient Map(Client client) => new(client.Id, client.Name, client.Email, client.Phone);
    private Client Map(DbClient client) => new(client.Id, client.Name, client.Email, client.Phone);
    private DbNote Map(Note note) => new(note.Id, note.Content, note.JobId);
    private Note Map(DbNote note) => new(note.Id, note.Content, note.JobId);
}
