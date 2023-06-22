using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PurchasedProductEntity: BaseEntity
    {        
        public int Amount {get;set;}

        public Guid ProductId {get;set;}
        public ProductEntity Product { get; set; } = new();

        public Guid ProductPriceId {get;set;}
        public ProductPriceEntity ProductPrice { get; set; } = new();

    }
}