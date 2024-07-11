using Application.Repos;
using AutoMapper;
using MediatR;

namespace Application.Features.Tasks.Commands.Update;

public class UpdateTaskCommand : IRequest<UpdateTaskResponse>
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Domain.Entities.TaskStatus Status { get; set; }


    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, UpdateTaskResponse>
    {
        public readonly ITaskRepository _taskRepository;
        public readonly IMapper _mapper;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<UpdateTaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Task task = await _taskRepository.GetAsync(i => i.ID == request.ID);
            if (task == null)
                throw new Exception($"Task with ID {request.ID} not found.");
        
            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;

            await _taskRepository.UpdateAsync(task);

            UpdateTaskResponse response = _mapper.Map<UpdateTaskResponse>(task);
            return response;
        }
    }
}
