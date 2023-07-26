using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class PlanEntity: BaseEntity
    {
        
        public string Name{get; set;}        
        
        public int AmountOfDay{get; set;}        
        
        public string? Description{get; set;}

        public virtual List<PlanPriceEntity> Prices { get; set; } = new();
    }
}