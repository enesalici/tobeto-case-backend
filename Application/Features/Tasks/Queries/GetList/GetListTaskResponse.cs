namespace Application.Features.Tasks.Queries.GetList;

public class GetListTaskResponse
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public DateOnly? CreatedDate { get; set; }
}