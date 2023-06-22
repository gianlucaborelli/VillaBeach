using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class SaleEntity : BaseEntity
    {
        public List<SoldProductEntity> SoldProducts = new();
        
        public Guid UserId;
        public UserEntity User { get; set; } = new();
    }
}