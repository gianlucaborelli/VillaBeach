using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Purchase
{
    public class PurchaseDtoCreateResult
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool IsComplete { get; set; }
    }
}