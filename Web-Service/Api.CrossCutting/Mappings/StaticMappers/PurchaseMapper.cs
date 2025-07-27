using Api.Domain.Commands.PurchaseCommands;
using Api.Domain.Dtos.Purchase;
using Api.Domain.Entities;
using Domain.Dtos.Product;

namespace Api.CrossCutting.Mappings.StaticMappers
{
    public static class PurchaseMapper
    {
        public static PurchaseDto ToDto(this Purchase purchase)
        {
            if (purchase == null) return null!;

            return new PurchaseDto
            {
                Id = purchase.Id,
                UserId = purchase.UserId,
                PurchasedProducts = purchase.PurchasedProducts?.Select(pp => pp.ToDto()).ToList() ?? new List<PurchasedProductDto>()
            };
        }

        public static Purchase ToEntity(this PurchaseDto dto)
        {
            if (dto == null) return null!;

            return new Purchase
            {
                Id = dto.Id,
                UserId = dto.UserId,
                PurchasedProducts = dto.PurchasedProducts?.Select(pp => pp.ToEntity()).ToList()
            };
        }

        public static CreatePurchaseCommand ToCreateCommand(this PurchaseDtoCreateRequest request)
        {
            if (request == null) return null!;

            return new CreatePurchaseCommand
            {
                PurchasedProducts = request.PurchasedProducts?.Select(pp => new PurchasedProductDto
                {
                    ProductId = pp.ProductId,
                    Amount = pp.Amount,
                    Price = pp.Price
                }).ToList() ?? new List<PurchasedProductDto>()
            };
        }

        public static UpdatePurchaseCommand ToUpdateCommand(this PurchaseDtoUpdateRequest request)
        {
            if (request == null) return null!;

            return new UpdatePurchaseCommand
            {
                Id = request.Id,
                PurchasedProducts = request.PurchasedProducts?.Select(pp => new UpdatePurchaseCommand.PurchasedProductData
                {
                    ProductId = pp.ProductId,
                    Amount = pp.Amount,
                    Price = pp.Price
                }).ToList() ?? new List<UpdatePurchaseCommand.PurchasedProductData>()
            };
        }

        public static IEnumerable<PurchaseDto> ToDtoList(this IEnumerable<Purchase> purchases)
        {
            return purchases?.Select(p => p.ToDto()) ?? Enumerable.Empty<PurchaseDto>();
        }
    }

    public static class PurchasedProductMapper
    {
        public static PurchasedProductDto ToDto(this PurchasedProduct purchasedProduct)
        {
            if (purchasedProduct == null) return null!;

            return new PurchasedProductDto
            {
                Id = purchasedProduct.Id,
                ProductId = purchasedProduct.ProductId,
                Amount = purchasedProduct.Amount,
                Price = purchasedProduct.Price
            };
        }

        public static PurchasedProduct ToEntity(this PurchasedProductDto dto)
        {
            if (dto == null) return null!;

            return new PurchasedProduct
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                Amount = dto.Amount,
                Price = dto.Price
            };
        }

        public static PurchasedProductDto ToDto(this PurchasedProductDtoCreateRequest request)
        {
            if (request == null) return null!;

            return new PurchasedProductDto
            {
                ProductId = request.ProductId,
                Amount = request.Amount,
                Price = request.Price
            };
        }
    }
} 