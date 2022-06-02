using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataModel;

public class DbNote : BaseModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; }
    [ForeignKey(nameof(DbJob))]
    public Guid JobId { get; set; }

    public DbNote(Guid id, string content, Guid jobId)
    {
        Id = id;
        Content = content;
        JobId = jobId;
    }

    public DbNote()
    {
    }

    public void Update(DbNote note)
    {
        Id = note.Id;
        Content = note.Content;
        JobId = note.JobId;
    }
}

