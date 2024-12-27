using RealtimeChatApp.Models;

namespace RealtimeChatApp.Repository.Base
{
    public interface IBaseRepository<in TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
    {
        Task CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAsync(int take = int.MaxValue, int skip = 0, CancellationToken cancellationToken = default);
    }
}
