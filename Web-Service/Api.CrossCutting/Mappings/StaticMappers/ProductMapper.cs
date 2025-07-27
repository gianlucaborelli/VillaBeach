using Api.Domain.Dtos.Product;
using Api.Domain.Entities;

namespace Api.CrossCutting.Mappings.StaticMappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product)
        {
            if (product == null) return null!;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public static Product ToEntity(this ProductDto dto)
        {
            if (dto == null) return null!;

            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                BarCode = dto.BarCode,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }

        public static ProductDtoCreateResult ToCreateResult(this Product product)
        {
            if (product == null) return null!;

            return new ProductDtoCreateResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public static ProductDtoUpdateResult ToUpdateResult(this Product product)
        {
            if (product == null) return null!;

            return new ProductDtoUpdateResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public static ProductDtoAvailableResult ToAvailableResult(this Product product)
        {
            if (product == null) return null!;

            return new ProductDtoAvailableResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BarCode = product.BarCode,
                Price = product.Price,
                Stock = product.Stock,
                Available = product.Stock > 0
            };
        }

        public static Product ToEntity(this ProductDtoCreateRequest dto)
        {
            if (dto == null) return null!;

            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                BarCode = dto.BarCode,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }

        public static Product ToEntity(this ProductDtoUpdateRequest dto)
        {
            if (dto == null) return null!;

            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                BarCode = dto.BarCode,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }

        public static IEnumerable<ProductDto> ToDtoList(this IEnumerable<Product> products)
        {
            return products?.Select(p => p.ToDto()) ?? Enumerable.Empty<ProductDto>();
        }

        public static IEnumerable<ProductDtoAvailableResult> ToAvailableResultList(this IEnumerable<Product> products)
        {
            return products?.Select(p => p.ToAvailableResult()) ?? Enumerable.Empty<ProductDtoAvailableResult>();
        }
    }
} 