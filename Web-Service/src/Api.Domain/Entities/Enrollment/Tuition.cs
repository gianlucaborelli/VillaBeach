namespace Api.Domain.Entities
{
    public class Tuition
    {
        public Guid Id { get; set;}
        
        public Guid PlanId { get; set; }
        public Plan Plan { get; set;}

        public Guid PriceId { get; set; }
        public PlanPrice Price { get; set;} = new();
    }
}