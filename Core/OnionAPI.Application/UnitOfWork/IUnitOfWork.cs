using OnionAPI.Application.Interfaces.Repositories;
using OnionAPI.Domain.Common;

namespace OnionAPI.Application.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new();
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
    Task<int> SaveAsync();
    int Save();
}
