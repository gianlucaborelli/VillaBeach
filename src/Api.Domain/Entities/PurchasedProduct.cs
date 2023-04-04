using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PurchasedProduct: BaseEntity
    {
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public Guid ProductId;
        public Product Product { get; set; } = new();

        public Guid ProductPriceId;
        public ProductPrice ProductPrice { get; set; } = new();

    }
}