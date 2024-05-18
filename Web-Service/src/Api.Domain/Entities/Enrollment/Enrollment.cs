using Api.Core.Domain;

namespace Api.Domain.Entities
{
    public class Enrollment : Entity, IAggregateRoot 
    {           
        public Guid UserId;
        public User User{get;set;}

        public Guid TuitionId {get; set;}
        public Tuition Tuition { get; set; }
        
        public DateTime Deadline{get; set;}
    }
}