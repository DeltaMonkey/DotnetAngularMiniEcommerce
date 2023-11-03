namespace DotnetAngularMiniEcommerce_API.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T: class
    {
    }
}
