using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class AddressEntity: BaseEntity
    {
        
        public string PostalCode {get; set;}
        
        public string Street {get; set;}
        
        public string Number{get; set;}
        
        public string District{get; set;}
                
        public string City{get; set;}

        
        public string State{get; set;}        
        
        public string? Description{get; set;}
                      
        public Guid UserId {get; set;}
        public UserEntity User {get;set;} = new();
    }
}