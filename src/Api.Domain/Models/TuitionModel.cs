using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class TuitionModel : BaseModel
    {
        private Guid _planId;
        public Guid PlanId
        {
            get { return _planId; }
            set { _planId = value; }
        }

        private Guid _priceId;
        public Guid PriceId
        {
            get { return _priceId; }
            set { _priceId = value; }
        }
    }
}