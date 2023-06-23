using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities.UserEntityEnum;

namespace Api.Domain.Models
{
    public class ContactModel : BaseModel
    {

        private ContactTypeEnum _contactType;
        public ContactTypeEnum ContactType
        {
            get { return _contactType; }
            set { _contactType = value; }
        }
        
        private string _contactForm = string.Empty;
        public string ContactForm
        {
            get { return _contactForm; }
            set { _contactForm = value; }
        }
        
        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }        
        
        public Guid UserId;
    }
}