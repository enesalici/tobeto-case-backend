using Core.Base.Entities;


namespace Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string Username { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public virtual ICollection<Task> Tasks { get; set; }

}
