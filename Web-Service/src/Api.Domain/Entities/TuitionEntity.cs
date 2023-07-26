using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class TuitionEntity : BaseEntity
    {
        [ForeignKey("Plan")]
        public Guid PlanId { get; set; }
        public PlanEntity Plan { get; set;}

        [ForeignKey("PlanPrice")]
        public Guid PriceId { get; set; }
        public PlanPriceEntity Price { get; set;} = new();
    }
}