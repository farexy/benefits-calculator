using Api.Db;
using Api.Models;
using Api.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class DependentsRepository : IDependentsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DependentsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dependent?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Dependents
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<List<Dependent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Dependents.ToListAsync(cancellationToken);
    }
}