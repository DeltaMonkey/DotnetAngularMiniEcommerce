using Microsoft.EntityFrameworkCore;

namespace DotnetAngularMiniEcommerce_API.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}
