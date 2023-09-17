using Api.Application.Employee;
using Api.Dtos.Employee;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        return await _mediator.Send(new GetEmployeeRequest(id));
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        return await _mediator.Send(new GetAllEmployeesRequest());
    }
    
    [SwaggerOperation(Summary = "Get employee paycheck")]
    [HttpGet("{id}/paycheck")]
    public async Task<ActionResult<ApiResponse<GetEmployeePaycheckDto>>> GetEmployeePaycheck(int id)
    {
        return await _mediator.Send(new CalculateEmployeePaycheckRequest(id));
    }
}
