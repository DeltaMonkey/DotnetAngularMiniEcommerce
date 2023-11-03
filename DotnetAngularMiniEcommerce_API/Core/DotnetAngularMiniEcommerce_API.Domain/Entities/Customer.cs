using DotnetAngularMiniEcommerce_API.Domain.Entities.Common;

namespace DotnetAngularMiniEcommerce_API.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public ICollection<Order> Orders { get; set; }
    }
}
