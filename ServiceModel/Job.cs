using DataAccess.DataModel;

namespace ServiceModel;

public record Job(Guid Id, JobStatus Status, Guid ClientId);
public record JobView(Guid Id, JobStatus Status, string ClientName);