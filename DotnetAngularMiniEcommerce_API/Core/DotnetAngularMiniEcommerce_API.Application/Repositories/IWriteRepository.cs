using DotnetAngularMiniEcommerce_API.Domain.Entities.Common;

namespace DotnetAngularMiniEcommerce_API.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T: BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Remove(T entity);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> entities);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}
