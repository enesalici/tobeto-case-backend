using Application.Features.Tasks.Commands.Create;
using Application.Features.Tasks.Commands.Delete;
using Application.Features.Tasks.Commands.Update;
using Application.Features.Tasks.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class TaskController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand Command)
    {
       CreateTaskResponse response = await _mediator.Send(Command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
    {
        UpdateTaskResponse response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
    {
        var command = new DeleteTaskCommand { ID = id };
        await _mediator.Send(command);
        return Ok("Task deleted successfully");
    }

    [HttpGet("GetList/{userId}")]
    public async Task<IActionResult> GetListTask([FromRoute] Guid userId)
    {
        var query = new GetListTaskQuery { UserId = userId };
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}