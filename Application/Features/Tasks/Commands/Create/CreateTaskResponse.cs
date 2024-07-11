namespace Application.Features.Tasks.Commands.Create
{
    public class CreateTaskResponse
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateOnly? CreatedDate { get; set; }
    }
}