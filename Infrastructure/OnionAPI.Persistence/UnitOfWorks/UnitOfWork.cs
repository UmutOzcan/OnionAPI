using OnionAPI.Application.Interfaces.Repositories;
using OnionAPI.Application.Interfaces.UnitOfWork;
using OnionAPI.Persistence.Context;
using OnionAPI.Persistence.Repositories;

namespace OnionAPI.Persistence.UnitOfWorks;

// Bu pattern, business katmanında yapılan her değişikliğin anlık olarak database e yansıması yerine,
// işlemlerin toplu halde tek bir kanaldan gerçekleşmesini sağlar.
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    public int Save() => _dbContext.SaveChanges();

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_dbContext);

    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_dbContext);
}
