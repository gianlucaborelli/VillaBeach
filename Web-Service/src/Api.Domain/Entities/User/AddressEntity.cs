using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class AddressEntity
    {        
        public required string PostalCode {get; set;}
        
        public required string Street {get; set;}
        
        public required string Number{get; set;}
        
        public required string District{get; set;}
                
        public required string City{get; set;}
        
        public required string State{get; set;}        
        
        public string? Description{get; set;}          
    }
}