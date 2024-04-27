using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO;

public class RegistroDTO
{
    [Required(ErrorMessage = "Usuário é obrigatório")]
    public string Usuario { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    public string Senha { get; set; }
}
