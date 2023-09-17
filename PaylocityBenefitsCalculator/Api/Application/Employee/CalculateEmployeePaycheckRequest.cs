using Api.Dtos.Employee;
using Api.Errors;
using Api.Exceptions;
using Api.Helpers;
using Api.Models;
using Api.Repositories.Abstract;
using MediatR;

namespace Api.Application.Employee;

public record CalculateEmployeePaycheckRequest(int Id) : IRequest<ApiResponse<GetEmployeePaycheckDto>>;

public class CalculatePaycheckRequestHandler : IRequestHandler<CalculateEmployeePaycheckRequest, ApiResponse<GetEmployeePaycheckDto>>
{
    private readonly IPaycheckCalculator _paycheckCalculator;
    private readonly IEmployeesRepository _employeesRepository;

    public CalculatePaycheckRequestHandler(IPaycheckCalculator paycheckCalculator, IEmployeesRepository employeesRepository)
    {
        _paycheckCalculator = paycheckCalculator;
        _employeesRepository = employeesRepository;
    }

    public async Task<ApiResponse<GetEmployeePaycheckDto>> Handle(CalculateEmployeePaycheckRequest request, CancellationToken cancellationToken)
    {
        var entity = await _employeesRepository.GetAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new EntityNotFoundException(ErrorCodes.NotFound, "Entity not found");
        }

        return new ApiResponse<GetEmployeePaycheckDto>
        {
            Data = new GetEmployeePaycheckDto
            {
                Value = _paycheckCalculator.CalculatePaycheck(entity),
            }
        };
    }
}