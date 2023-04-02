using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Plan: BaseEntity
    {
        private string _name;
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
        
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }        

        public virtual List<PlanPrice> Prices { get; set; } = new();
    }
}