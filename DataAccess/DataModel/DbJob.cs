using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataModel;

public class DbJob : BaseModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public JobStatus Status { get; set; }
    [ForeignKey(nameof(DbClient))]
    public Guid ClientId { get; set; }
    public virtual DbClient Client { get; set; }
    public virtual List<DbNote> Notes { get; set; } = new List<DbNote>();

    public DbJob(Guid id, JobStatus status, Guid clientId)
    {
        Id = id;
        Status = status;
        ClientId = clientId;
    }

    public DbJob()
    {
    }

    public void Update(DbJob job)
    {
        Id = job.Id;
        Status = job.Status;
        ClientId = job.ClientId;
    }
}

public enum JobStatus
{
    SCHEDULED,
    ACTIVE,
    INVOICING,
    TOPRICED,
    COMPLETED
}
