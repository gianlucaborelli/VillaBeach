using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class PurchasedProductModel : BaseModel
    {
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private Guid _productId;
        public Guid ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
        
        private Guid _productPriceId;
        public Guid ProductPriceId
        {
            get { return _productPriceId; }
            set { _productPriceId = value; }
        }
    }
}