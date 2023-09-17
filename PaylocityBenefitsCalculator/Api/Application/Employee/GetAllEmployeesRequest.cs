using Api.Dtos.Employee;
using Api.Models;
using Api.Repositories.Abstract;
using AutoMapper;
using MediatR;

namespace Api.Application.Employee;

public record GetAllEmployeesRequest : IRequest<ApiResponse<List<GetEmployeeDto>>>;

public class GetAllEmployeesRequestHandler : IRequestHandler<GetAllEmployeesRequest, ApiResponse<List<GetEmployeeDto>>>
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;

    public GetAllEmployeesRequestHandler(IEmployeesRepository employeesRepository, IMapper mapper)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetEmployeeDto>>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
    {
        var data = await _employeesRepository.GetAllAsync(cancellationToken);

        return new ApiResponse<List<GetEmployeeDto>>
        {
            Data = _mapper.Map<List<GetEmployeeDto>>(data)
        };
    }
}
