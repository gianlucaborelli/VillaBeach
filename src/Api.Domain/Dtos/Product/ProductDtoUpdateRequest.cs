using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Product
{
    public class ProductDtoUpdateRequest
    {
        [Required(ErrorMessage = "Id é Obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Código de Barras é obrigatório.")]
        public string? BarCode { get; set; }
    }
}