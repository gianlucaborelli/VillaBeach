using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {        
        public string Name{get;set;}

        public string? Description{get;set;}

        public int Stock{get;set;}
        
        public string? BarCode{get;set;}

        public virtual List<ProductPriceEntity> Prices { get; set; } = new();
    }
}