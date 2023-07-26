using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.ProductPrice
{
    public class ProductPriceDtoAvailableResult
    {
        public Guid Id { get; set; }    
        public decimal Value { get; set; }
    }
}