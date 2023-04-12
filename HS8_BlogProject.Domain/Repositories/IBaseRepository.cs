using HS8_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HS8_BlogProject.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity); // Veritabanından silme işlemi yapmak yerine status pasife çekilir.
        Task<bool> Any(Expression<Func<TEntity, bool>> expression); // Kayıt varsa true, toksa false döner
        Task<TEntity> GetDefault(Expression<Func<TEntity, bool>> expression); // Dinamik olarak where işlemi sağlar.
        Task<List<TEntity>> GetDefaults(Expression<Func<TEntity, bool>> expression); // Dinamik olarak entity listesi döner.
        Task<TResult> GetFilteredFirstOrDefault<TResult>( // Select, Where, Order By, Join
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<List<TResult>> GetFilteredList<TResult>( // Select, Where, Order By, Join
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}
