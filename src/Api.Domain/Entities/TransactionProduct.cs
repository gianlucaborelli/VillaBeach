using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class TransactionProduct : BaseEntity
    {
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        [ForeignKey("Product")]
        public Guid _productId;
        public Product Product { get; set; }

        [ForeignKey("Price")]
        public Guid _priceId;
        public Price Price { get; set; }


    }
}