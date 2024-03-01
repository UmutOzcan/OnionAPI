using Microsoft.EntityFrameworkCore.Query;
using OnionAPI.Domain.Common;
using System.Linq.Expressions;

namespace OnionAPI.Application.Interfaces.Repositories;

public interface IReadRepository<T> where T : class, IEntityBase, new()
{
    // predicate ile varlıkların koşulları için, include eager loading için
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false
        );

    Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false,
        int currentPage = 1,
        int pageSize = 3
        );

    // tek bir varlik alacagindan Nullable olmasına gerek yok
    Task<T> GetAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = false
        );

    IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}
