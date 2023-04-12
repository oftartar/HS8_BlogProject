using HS8_BlogProject.Domain.Entities;
using HS8_BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HS8_BlogProject.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        // Context bağlantısına ihtiyacımız var
        private readonly AppDbContext _context;
        protected DbSet<TEntity> _entities;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }

        public async Task Create(TEntity entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetDefault(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetDefaults(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _entities;
            if (where != null)
                query = query.Where(where);
            if (include != null)
                query = include(query);
            if (orderBy != null)
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            else
                return await query.Select(select).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _entities;
            if (where != null)
                query = query.Where(where);
            if (include != null)
                query = include(query);
            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();
            else
                return await query.Select(select).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            //_context.Update(entity);
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
