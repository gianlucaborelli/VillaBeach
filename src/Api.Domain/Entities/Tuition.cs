using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Tuition : BaseEntity
    {
        [ForeignKey("Plan")]
        public Guid PlanId { get; set; }
        public Plan Plan { get; set;}

        [ForeignKey("PlanPrice")]
        public Guid PriceId { get; set; }
        public PlanPrice Price { get; set;} = new();
    }
}