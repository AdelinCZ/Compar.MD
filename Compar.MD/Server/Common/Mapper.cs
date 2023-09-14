using Compar.MD.Domain.Models;
using Compar.MD.Shared;

namespace Compar.MD.Server.Mapping
{
    public static class Mapper
    {
        public static List<ProductDto> GetAllProducts(List<Product> products)
        {
            var productsDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                var productdto = new ProductDto()
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    ProductBrand = product.Brand,
                    ProductType = product.Type,
                    ImageURL = product.ImageURL,
                    IsSelected = product.IsSelected
                };

                foreach (var price in product.Suppliers)
                {
                    var pricedto = new PriceDto()
                    {
                        Price = price.Price,
                        SupplierName = price.StoreName
                    };

                    productdto.Prices.Add(pricedto);
                }

                productsDtos.Add(productdto);
            }

            return productsDtos;
        }

        public static List<QuestionDto> GetQuestions(List<Question> questions)
        {
            var questionsDto = new List<QuestionDto>();

            foreach (var question in questions)
            {
                var questionDto = new QuestionDto()
                {
                    Id = question.Id.ToString(),
                    Question = question.Value,
                    Answer = question.Answer
                };

                questionsDto.Add(questionDto);
            }

            return questionsDto;
        }

        public static List<StoreDto> GetStores(List<Store> stores)
        {
            var storesDto = new List<StoreDto>();

            foreach (var store in stores)
            {
                var dto = new StoreDto()
                {
                    Id = store.Id.ToString(),
                    StoreName = store.Name
                };

                storesDto.Add(dto);
            }

            return storesDto;
        }

        public static List<BrandDto> GetBrands(List<Brand> brands)
        {
            var brandsDto = new List<BrandDto>();

            foreach (var brand in brands)
            {
                var dto = new BrandDto()
                {
                    Id = brand.Id.ToString(),
                    BrandName = brand.Name
                };

                brandsDto.Add(dto);
            }

            return brandsDto;
        }

        public static List<ProductTypeDto> GetProductTypes(List<ProductType> productTypes)
        {
            var productTypeDto = new List<ProductTypeDto>();

            foreach (var productType in productTypes)
            {
                var dto = new ProductTypeDto()
                {
                    Id = productType.Id.ToString(),
                    ProductTypeName = productType.Name
                };

                productTypeDto.Add(dto);
            }

            return productTypeDto;
        }
    }
}