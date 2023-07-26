using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class PurchaseModel : BaseModel
    {

        private List<PurchasedProductModel> _purchasedProductsList = new();
        public List<PurchasedProductModel> PurcgasedProductsList
        {
            get { return _purchasedProductsList; }
            set { _purchasedProductsList = value; }
        }

        private Guid _userId;
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private bool _isCompete;
        public bool IsComplete
        {
            get { return _isCompete; }
            set { _isCompete = value; }
        }
        
    }
}