using Microsoft.EntityFrameworkCore;
using Technical.Business.Domain;

namespace Technical.Business.Data.Repository.GenericRepository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;
        protected DbContext DbContext => _dbContext;
        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task<Response<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken= default(CancellationToken))
        {
            return new Response<IEnumerable<TEntity>>(await _dbSet.ToListAsync(cancellationToken));
        }

        public async Task<Response<TEntity?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new Response<TEntity?>(await _dbSet.FindAsync(new object[1] { id }, cancellationToken));
        }


        public async Task<Response<TEntity?>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            if (DbContext.Database.CurrentTransaction == null)
            {
                if ((await SaveChangesAsync(cancellationToken)).IsSuccess)
                {
                    return new Response<TEntity?>(entity);
                }

                return new Response<TEntity?>(null, false);
            }

            return new Response<TEntity?>(entity);
        }

        public async Task<Response<int>> InsertAsync(TEntity[] entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddRangeAsync(entities);
            if (DbContext.Database.CurrentTransaction == null)
            {
                return await SaveChangesAsync(cancellationToken);
            }

            return new Response<int>(0);
        }


        public async Task<Response<int>> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            TEntity val = await _dbSet.FindAsync(new object[1] { id }, cancellationToken);
            if (val == null)
            {
                return new Response<int>(0, false);
            }

            DbContext.Entry(val).CurrentValues.SetValues(entity);
            if (DbContext.Database.CurrentTransaction == null)
            {
                return await SaveChangesAsync(cancellationToken);
            }

            return new Response<int>(0);
        }

        public async Task<Response<int>> UpdateAsync(TEntity[] entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            _dbSet.UpdateRange(entities);
            if (DbContext.Database.CurrentTransaction == null)
            {
                return await SaveChangesAsync(cancellationToken);
            }

            return new Response<int>(0);
        }

        public async Task<Response<int>> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                int num = await _dbContext.SaveChangesAsync(cancellationToken);
                if (num == 0)
                {
                    return new Response<int>(num);
                }

                return new Response<int>(num);
            }
            catch (Exception)
            {
                return new Response<int>(0, false);
            }
        }
    }
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<Response<int>> SaveChangesAsync(CancellationToken cancellationToken);
        Task<Response<TEntity?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Response<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Response<TEntity?>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<Response<int>> InsertAsync(TEntity[] entities, CancellationToken cancellationToken);

        Task<Response<int>> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken);

        Task<Response<int>> UpdateAsync(TEntity[] entities, CancellationToken cancellationToken);

    }
    

}
