using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Attributes;

namespace Api.Domain.Dtos.Product
{
    public class ProductDtoCreateRequest
    {
        [Required(ErrorMessage ="Nome é obrigatório.")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage ="Código de Barras é obrigatório.")]
        [EAN13]
        public string? BarCode { get; set; }
    }
}