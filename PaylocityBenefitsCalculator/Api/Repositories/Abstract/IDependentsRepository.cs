using Api.Models;

namespace Api.Repositories.Abstract;

public interface IDependentsRepository
{
    Task<Dependent?> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<Dependent>> GetAllAsync(CancellationToken cancellationToken);
}