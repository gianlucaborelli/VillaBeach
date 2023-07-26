using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class EnrollmentModel : BaseModel
    {
        private Guid _userId;
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private Guid _tuitionId;
        public Guid TuitionId
        {
            get { return _tuitionId; }
            set { _tuitionId = value; }
        }

        private DateTime _deadline;
        public DateTime Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }


    }
}