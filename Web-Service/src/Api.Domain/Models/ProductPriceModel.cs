using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class ProductPriceModel : BaseModel
    {
        private decimal _value;
        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private bool _current;
        public bool Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private Guid _productId;
        public Guid ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
    }
}