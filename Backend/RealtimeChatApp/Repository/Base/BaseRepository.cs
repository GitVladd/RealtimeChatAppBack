using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.Data;
using RealtimeChatApp.Models;

namespace RealtimeChatApp.Repository.Base
{
    public abstract class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();


        protected BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            Entities.Add(entity);
            await SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(int take = int.MaxValue, int skip = 0, CancellationToken cancellationToken = default)
        {
            if (take < 0) throw new ArgumentException("Invalid take parameter: must be non-negative.");
            if (skip < 0) throw new ArgumentException("Invalid skip parameter: must be non-negative.");

            return await Entities.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
