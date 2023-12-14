using DotnetAngularMiniEcommerce_API.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DotnetAngularMiniEcommerce_API.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
