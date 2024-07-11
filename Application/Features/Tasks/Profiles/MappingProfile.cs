using Application.Features.Tasks.Commands.Create;
using Application.Features.Tasks.Commands.Delete;
using Application.Features.Tasks.Commands.Update;
using Application.Features.Tasks.Queries.GetList;
using AutoMapper;

namespace Application.Features.Tasks.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTaskCommand, Domain.Entities.Task>().ReverseMap();
        CreateMap<CreateTaskResponse, Domain.Entities.Task>().ReverseMap();
        CreateMap<DeleteTaskCommand, Domain.Entities.Task>().ReverseMap();
        CreateMap<UpdateTaskResponse, Domain.Entities.Task>().ReverseMap();
        CreateMap<UpdateTaskResponse, Domain.Entities.Task>().ReverseMap();
        CreateMap<GetListTaskQuery, Domain.Entities.Task>().ReverseMap();
        CreateMap<GetListTaskResponse, Domain.Entities.Task>().ReverseMap();

    }
}
