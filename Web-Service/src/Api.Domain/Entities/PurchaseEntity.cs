using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PurchaseEntity : BaseEntity
    {   
        public Guid UserId {get;set;}
        public UserEntity User { get; set; } 

        public bool IsComplete {get; set;} = false;

        public List<PurchasedProductEntity> PurchasedProducts {get;set;}
    }
}