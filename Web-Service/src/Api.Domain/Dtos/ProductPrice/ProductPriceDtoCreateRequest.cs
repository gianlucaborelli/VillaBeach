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
        [DecimalPrecision(2)]
        public decimal Value{get;set;}
        
        public Guid ProductId {get;set;}

    }
}