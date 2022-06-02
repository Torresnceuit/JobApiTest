using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataModel;

public class DbClient : BaseModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public virtual List<DbJob> Jobs { get; set; } = new List<DbJob>();

    public DbClient(Guid id, string name, string email, string phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
    }

    public DbClient()
    {
    }

    public void Update(DbClient client)
    {
        Id = client.Id;
        Name = client.Name;
        Email = client.Email;
        Phone = client.Phone;
    }
}

