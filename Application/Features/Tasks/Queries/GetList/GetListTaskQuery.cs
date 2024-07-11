using Application.Repos;
using AutoMapper;
using MediatR;

namespace Application.Features.Tasks.Queries.GetList;

public class GetListTaskQuery : IRequest<List<GetListTaskResponse>>
{
    public Guid UserId { get; set; }
    public class GetListUserQueryHandler : IRequestHandler<GetListTaskQuery, List<GetListTaskResponse>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetListUserQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListTaskResponse>> Handle(GetListTaskQuery request, CancellationToken cancellationToken)
        {
            
            List<Domain.Entities.Task> task = await _taskRepository.GetListAsync(t => t.UserId == request.UserId);

            if (task == null || task.Count == 0)
                throw new Exception($"No tasks found for user with ID {request.UserId}.");

            List<GetListTaskResponse> response = _mapper.Map<List<GetListTaskResponse>>(task);
            return response;
        }
    }
}