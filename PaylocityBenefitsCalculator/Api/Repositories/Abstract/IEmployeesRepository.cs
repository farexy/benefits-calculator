using Api.Models;

namespace Api.Repositories.Abstract;

public interface IEmployeesRepository
{
    Task<Employee?> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);
}