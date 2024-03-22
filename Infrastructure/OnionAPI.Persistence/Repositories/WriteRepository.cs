using Microsoft.EntityFrameworkCore;
using OnionAPI.Application.Interfaces.Repositories;
using OnionAPI.Domain.Common;

namespace OnionAPI.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
{
    // DI
    private readonly DbContext _context;
    public WriteRepository(DbContext context)
    {
        _context = context;
    }

    // her seferinde _context.Set<T>() yazmak yerine Table yazarız
    private DbSet<T> Table { get => _context.Set<T>(); }
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task AddRangeAsync(IList<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public async Task HardDeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
    }

    public async Task HardDeleteRangeAsync(IList<T> entity)
    {
        await Task.Run(() => Table.RemoveRange(entity));
    }

    // Soft delete isini de update ile yaparız
    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => Table.Update(entity));
        return entity;
    }
}
