using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Product
{
    public class ProductDtoCreateRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? BarCode { get; set; }
    }
}