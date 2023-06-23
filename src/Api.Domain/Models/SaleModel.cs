using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class SaleModel : BaseModel
    {
        private List<SoldProductModel> _soldProductsList = new();
        public List<SoldProductModel> SoldProductsList
        {
            get { return _soldProductsList; }
            set { _soldProductsList = value; }
        }

        private Guid _userId;
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
    }
}