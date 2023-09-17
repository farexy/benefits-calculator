using Api.Db;
using Api.Models;
using Api.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Employee?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Employees
            .Include(e => e.Dependents)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Employees
            .Include(e => e.Dependents)
            .ToListAsync(cancellationToken);
    }
}