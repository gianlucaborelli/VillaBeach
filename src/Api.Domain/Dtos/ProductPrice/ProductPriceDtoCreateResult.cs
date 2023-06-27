using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.ProductPrice
{
    public class ProductPriceDtoCreateResult
    {
        public Guid Id { get; set; }
        
        public decimal Value{get;set;}

        public bool Current{get;set;}
                  
        public Guid ProductId {get;set;}

    }
}