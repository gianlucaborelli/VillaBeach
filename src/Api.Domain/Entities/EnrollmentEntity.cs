using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class EnrollmentEntity: BaseEntity
    {      

        [ForeignKey("User")]        
        public Guid UserId;
        public UserEntity User{get;set;}

        [ForeignKey("Tuition")]
        public Guid TuitionId {get; set;}
        public TuitionEntity Tuition { get; set; }
        
        public DateTime Deadline{get; set;}
    }
}