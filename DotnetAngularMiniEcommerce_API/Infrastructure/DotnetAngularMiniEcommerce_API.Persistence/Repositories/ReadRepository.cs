using DotnetAngularMiniEcommerce_API.Application.Repositories;
using DotnetAngularMiniEcommerce_API.Domain.Entities.Common;
using DotnetAngularMiniEcommerce_API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotnetAngularMiniEcommerce_API.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext _context;

        public ReadRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method) => Table.Where(method);

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> method) => await Table.FirstOrDefaultAsync(method);

        public async Task<T> GetByIdAsync(string id) => await Table.FindAsync(Guid.Parse(id));
    }
}
