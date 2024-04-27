using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO;

public class LoginDTO
{
    [Required(ErrorMessage ="Usuário é obrigatório")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    public string Senha { get; set; }
}
