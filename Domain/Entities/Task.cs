using Core.Base.Entities.Auditing;
using System.ComponentModel;


namespace Domain.Entities;

public class Task : AuditableEntity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

}

public enum TaskStatus
{
    [Description("new")]
    New = 1,

    [Description("InProgress")]
    InProgress = 2,

    [Description("Completed")]
    Completed = 3
}
