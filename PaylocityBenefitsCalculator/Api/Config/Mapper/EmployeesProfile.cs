using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Config.Mapper;

public class EmployeesProfile : Profile
{
    public EmployeesProfile()
    {
        CreateMap<Employee, GetEmployeeDto>();
    }
}