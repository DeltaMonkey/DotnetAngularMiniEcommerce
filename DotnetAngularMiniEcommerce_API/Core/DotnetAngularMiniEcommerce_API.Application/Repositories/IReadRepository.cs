using System.Linq.Expressions;

namespace DotnetAngularMiniEcommerce_API.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> method);
        Task<T> GetByIdAsync(int id);
    }
}
