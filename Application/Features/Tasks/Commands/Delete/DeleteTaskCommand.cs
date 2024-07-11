using Application.Repos;
using MediatR;

namespace Application.Features.Tasks.Commands.Delete;

public class DeleteTaskCommand : IRequest
{
    public Guid ID { get; set; }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Task task = await _taskRepository.GetAsync(i => i.ID == request.ID);
            if (task == null)
                throw new Exception($"Task with ID {request.ID} not found.");

            await _taskRepository.DeleteAsync(task);
        }
    }
}
