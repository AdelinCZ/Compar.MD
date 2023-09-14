using Compar.MD.Domain.Common;

namespace Compar.MD.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string ImageURL { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsSelected { get; set; }
        public List<Supplier> Suppliers { get; set; } = new();
    }
}