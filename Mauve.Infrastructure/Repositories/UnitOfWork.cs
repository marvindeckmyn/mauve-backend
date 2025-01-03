using Mauve.Core.Interfaces;
using Mauve.Infrastructure.Data;

namespace Mauve.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MauveDbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(MauveDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new GenericRepository<TEntity>(_context);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}