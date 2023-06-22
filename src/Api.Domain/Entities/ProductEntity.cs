using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private int _stock;
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
       
        private string? _barCode;
        public string? BarCode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

        public virtual List<ProductPriceEntity> Prices { get; set; } = new();
    }
}