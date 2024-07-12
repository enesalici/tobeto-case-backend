using Application.Repos;
using AutoMapper;
using Core.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tasks.Commands.Create;

public class CreateTaskCommand : IRequest<CreateTaskResponse> 
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetAsync(i => i.ID == request.UserId);
        if (user == null)
            throw new Exception("user id invalid.");


        Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(request);

        task.Status = Domain.Entities.TaskStatus.New;

        await _taskRepository.AddAsync(task);

        CreateTaskResponse response = _mapper.Map<CreateTaskResponse>(task);

        return response;
    }
}
