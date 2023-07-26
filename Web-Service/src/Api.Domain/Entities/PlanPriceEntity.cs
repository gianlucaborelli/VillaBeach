using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PlanPriceEntity : BaseEntity
    {        
        public decimal Value{get;set;}

        public bool Current{get;set;}
     
        public Guid PlanId {get;set;}
        public PlanEntity Plan{get;set;} = new();
    }
}