using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;

namespace Api.Config.Mapper;

public class DependentsProfile : Profile
{
    public DependentsProfile()
    {
        CreateMap<Dependent, GetDependentDto>();
    }
}