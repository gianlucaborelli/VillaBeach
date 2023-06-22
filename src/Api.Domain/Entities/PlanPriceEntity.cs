using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PlanPriceEntity : BaseEntity
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
     
        public Guid PlanId;
        public PlanEntity Plan{get;set;} = new();
    }
}