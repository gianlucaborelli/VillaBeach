using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public List<TransactionProduct> TransactionProducts = new();


        [ForeignKey("User")]
        public Guid _userId;
        public UserEntity User { get; set; }
    }
}