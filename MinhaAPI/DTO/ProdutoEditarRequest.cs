using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO;

public record ProdutoEditarRequest : IValidatableObject
{
    [Range(1, 9999, ErrorMessage ="O estoque deve estar entre 1 e 9999")]
    public float Estoque { get; set; }
    

    public DateTime DataCadastro { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(DataCadastro < DateTime.Now)
        {
            yield return new ValidationResult("A data deve ser maior ou igual a data Atual", 
                new[] {nameof(this.DataCadastro)});
        }
    }
}
