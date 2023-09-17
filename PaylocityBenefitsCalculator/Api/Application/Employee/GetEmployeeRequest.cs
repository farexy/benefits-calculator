using Api.Dtos.Employee;
using Api.Errors;
using Api.Exceptions;
using Api.Models;
using Api.Repositories.Abstract;
using AutoMapper;
using MediatR;

namespace Api.Application.Employee;

public record GetEmployeeRequest(int Id) : IRequest<ApiResponse<GetEmployeeDto>>;

public class GetEmployeeRequestHandler : IRequestHandler<GetEmployeeRequest, ApiResponse<GetEmployeeDto>>
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;

    public GetEmployeeRequestHandler(IEmployeesRepository employeesRepository, IMapper mapper)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetEmployeeDto>> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _employeesRepository.GetAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new EntityNotFoundException(ErrorCodes.NotFound, "Entity not found");
        }

        return new ApiResponse<GetEmployeeDto>
        {
            Data = _mapper.Map<GetEmployeeDto>(entity)
        };
    }
}
