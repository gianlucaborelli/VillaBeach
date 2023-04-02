using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Entities
{
    public class Contact: BaseEntity
    {
        
        public ContactTypeEnum ContactType {get;set;}

        public string ContactForm {get;set;}
        
        public string Description {get;set;}

        [ForeignKey("User")]
        public Guid UserId;
        public UserEntity User {get; set;}

    }
}