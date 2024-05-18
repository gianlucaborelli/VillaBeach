namespace Api.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        public required string PostalCode {get; set;}
        
        public required string Street {get; set;}
        
        public required string Number{get; set;}
        
        public required string District{get; set;}
                
        public required string City{get; set;}
        
        public required string State{get; set;}        
        
        public string? Description{get; set;}          
    }
}