using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class AddAddressRequest
    {
        [Required (ErrorMessage = "UserId is required")]
        public required Guid UserId { get; set; }

        [Required (ErrorMessage = "PostalCode is required")]
        [StringLength(8, ErrorMessage = "PostalCode must have 8 characters")]
        public required string PostalCode {get; set;}
        
        [Required (ErrorMessage = "Street is required")]
        [StringLength(100, ErrorMessage = "Street must have between 3 and 100 characters", MinimumLength = 3)]        
        public required string Street {get; set;}
        
        [Required (ErrorMessage = "Number is required")]
        [StringLength(10, ErrorMessage = "Number must have between 1 and 10 characters", MinimumLength = 1)]
        public required string Number{get; set;}
        
        [StringLength(100, ErrorMessage = "District must have between 3 and 100 characters", MinimumLength = 3)]
        [Required (ErrorMessage = "District is required")]
        public required string District{get; set;}

        [StringLength(100, ErrorMessage = "City must have between 3 and 100 characters", MinimumLength = 3)]
        public required string City{get; set;}
        
        [StringLength(2, ErrorMessage = "State must have 2 characters", MinimumLength = 2)]
        [Required (ErrorMessage = "State is required")]
        public required string State{get; set;}        
        
        [StringLength(200, ErrorMessage = "Complement must have between 3 and 200 characters")]
        public string? Description{get; set;}
    }
}