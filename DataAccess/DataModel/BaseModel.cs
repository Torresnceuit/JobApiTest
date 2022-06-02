namespace DataAccess.DataModel;
public class BaseModel
{
    /// <summary>
    /// The Time this object was created
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The Time this object was last updated
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// If this object has been deleted or not
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
