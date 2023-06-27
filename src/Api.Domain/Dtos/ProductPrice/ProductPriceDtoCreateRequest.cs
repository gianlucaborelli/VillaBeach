using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Attributes;

namespace Api.Domain.Dtos.ProductPrice
{
    public class ProductPriceDtoCreateRequest
    {        
        public decimal Value{get;set;}

        public bool Current{get;set;}
        
        public Guid ProductId {get;set;}

    }
}