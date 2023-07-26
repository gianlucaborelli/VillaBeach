using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class ProductPriceEntity :BaseEntity
    {
        public decimal Value{get;set;}

        public bool Current{get;set;}
                  
        public Guid ProductId {get;set;}

        public ProductEntity Product {get;set;}
    }
}