namespace Api.Domain.Entities
{
    public class PlanPrice
    {        
        public Guid Id { get; set; }
        public decimal Value{get;set;}

        public bool Current{get;set;}
     
        public Guid PlanId {get;set;}
        public Plan Plan{get;set;} = new();
    }
}