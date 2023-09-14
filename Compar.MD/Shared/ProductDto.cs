namespace Compar.MD.Shared
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<PriceDto> Prices { get; set; } = new List<PriceDto>(4);
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
        public string ImageURL { get; set; }
        public bool IsSelected { get; set; }
    }
}