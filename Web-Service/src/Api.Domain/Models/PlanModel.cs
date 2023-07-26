using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class PlanModel : BaseModel
    {
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _amountOfDay;
        public int AmountOfDay
        {
            get { return _amountOfDay; }
            set { _amountOfDay = value; }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private List<PlanPriceModel> _prices = new();
        public List<PlanPriceModel> Prices
        {
            get { return _prices; }
            set { _prices = value; }
        }
    }
}