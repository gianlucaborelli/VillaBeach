using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PurchaseEntity : BaseEntity
    {
        public List<PurchasedProductEntity> PurchasedProducts {get;set;} = new();
        
        public Guid UserId {get;set;}
        public UserEntity User { get; set; } = new();

    }
}