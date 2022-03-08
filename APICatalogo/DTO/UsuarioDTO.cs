using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Email é requerido")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatória")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimot {2} e no máximo " +
           "{1} caracteres.", MinimumLength = 10)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Você tem que confirmar a senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senha e confirmação não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}
