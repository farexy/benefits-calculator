using Api.Dtos.Dependent;
using Api.Models;
using Api.Repositories.Abstract;
using AutoMapper;
using MediatR;

namespace Api.Application.Dependent;

public record GetAllDependentsRequest : IRequest<ApiResponse<List<GetDependentDto>>>;

public class GetAllDependentsRequestHandler : IRequestHandler<GetAllDependentsRequest, ApiResponse<List<GetDependentDto>>>
{
    private readonly IDependentsRepository _dependentsRepository;
    private readonly IMapper _mapper;

    public GetAllDependentsRequestHandler(IDependentsRepository dependentsRepository, IMapper mapper)
    {
        _dependentsRepository = dependentsRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetDependentDto>>> Handle(GetAllDependentsRequest request, CancellationToken cancellationToken)
    {
        var data = await _dependentsRepository.GetAllAsync(cancellationToken);

        return new ApiResponse<List<GetDependentDto>>
        {
            Data = _mapper.Map<List<GetDependentDto>>(data)
        };
    }
}
