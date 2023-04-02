using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Enroll: BaseEntity
    {      

        [ForeignKey("User")]        
        public Guid UserId;
        public UserEntity User{get;set;}

        [ForeignKey("Tuition")]
        public Guid TuitionId {get; set;}
        public Tuition Tuition { get; set; }

        private DateTime _deadline;
        public DateTime Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }
    }
}