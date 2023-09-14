using Compar.MD.Domain.Common;

namespace Compar.MD.Domain.Models
{
    public class Supplier : BaseEntity
    {
        public string StoreName { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public DateTime LastUpdated { get; set; }

        public void Update(decimal price)
        {
            Price = price;
            LastUpdated = DateTime.UtcNow;
        }
    }
}