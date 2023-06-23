using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class ProductModel : BaseModel
    {
        private string _name = string.Empty;
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

        private List<ProductPriceModel> _prices = new();
        public List<ProductPriceModel> PriceModels
        {
            get { return _prices; }
            set { _prices = value; }
        }
    }
}