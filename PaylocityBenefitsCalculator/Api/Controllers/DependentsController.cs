using Api.Application.Dependent;
using Api.Dtos.Dependent;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DependentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetDependentDto>> Get(int id)
    {
        return await _mediator.Send(new GetDependentRequest(id));
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ApiResponse<List<GetDependentDto>>> GetAll()
    {
        return await _mediator.Send(new GetAllDependentsRequest());
    }
}
