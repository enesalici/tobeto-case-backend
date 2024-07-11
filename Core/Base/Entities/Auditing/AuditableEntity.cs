namespace Core.Base.Entities.Auditing;

public class AuditableEntity<T> : BaseEntity<T>
{
	public DateOnly CreatedDate { get; set; }
	public DateOnly? ModifiedDate { get; set; }
	public DateOnly? DeletedDate { get; set; }

}
