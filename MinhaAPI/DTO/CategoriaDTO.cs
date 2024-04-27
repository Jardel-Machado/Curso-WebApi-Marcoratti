using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO;

public record CategoriaDTO
{
    public int CategoriaId { get; set; }    
    public string? Nome { get; set; }    
    public string? ImagemUrl { get; set; }
}
