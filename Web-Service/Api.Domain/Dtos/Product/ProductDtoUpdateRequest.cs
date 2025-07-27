using System.ComponentModel.DataAnnotations;
using Api.Domain.Dtos.Attributes;

namespace Api.Domain.Dtos.Product
{
    public class ProductDtoUpdateRequest
    {
        [Required(ErrorMessage ="Id é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Nome é obrigatório.")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage ="Código de Barras é obrigatório.")]
        [EAN13]
        public string? BarCode { get; set; }

        [Required(ErrorMessage ="Preço é obrigatório.")]
        [DecimalPrecision(2)]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Estoque é obrigatório.")]
        public int Stock { get; set; }
    }
}