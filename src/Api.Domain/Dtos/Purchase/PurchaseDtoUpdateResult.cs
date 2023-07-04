using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDtoUpdateResult
    {
        public Guid Id { get; set; }
        
        public Guid UserId {get;set;}        

        public bool IsComplete {get; set;}

        public List<PurchasedProductEntity> PurchasedProducts {get;set;}
    }
}