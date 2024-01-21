
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Models
{
    public class UserEmailModel
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [StringLength(70, ErrorMessage ="E-mail deve ter no maximo {1} caracteres.")]
        [MinLength(5, ErrorMessage = "E-mail deve conter no mínimo {1} caracteres.")]
        private string _address = string.Empty;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private bool _emailIsVerified;
        public bool EmailIsVerified
        {
            get { return _emailIsVerified; }
            set { _emailIsVerified = value; }
        }
        

        private string? _emailVerificationToken;
        public string? EmailVerificationToken
        {
            get { return _emailVerificationToken; }
            set { _emailVerificationToken = value; }
        }

        private DateTime? _emailVerifiedAt;
        public DateTime? EmailVerifiedAt
        {
            get { return _emailVerifiedAt; }
            set { _emailVerifiedAt = value; }
        }
    }
}