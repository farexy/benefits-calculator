using Api.Dtos.Dependent;
using Api.Errors;
using Api.Exceptions;
using Api.Models;
using Api.Repositories.Abstract;
using AutoMapper;
using MediatR;

namespace Api.Application.Dependent;

public record GetDependentRequest(int Id) : IRequest<ApiResponse<GetDependentDto>>;

public class GetDependantRequestHandler : IRequestHandler<GetDependentRequest, ApiResponse<GetDependentDto>>
{
    private readonly IDependentsRepository _dependentsRepository;
    private readonly IMapper _mapper;

    public GetDependantRequestHandler(IDependentsRepository dependentsRepository, IMapper mapper)
    {
        _dependentsRepository = dependentsRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GetDependentDto>> Handle(GetDependentRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dependentsRepository.GetAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new EntityNotFoundException(ErrorCodes.NotFound, "Entity not found");
        }

        return new ApiResponse<GetDependentDto>
        {
            Data = _mapper.Map<GetDependentDto>(entity)
        };
    }
}
