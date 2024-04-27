using APICatalogo.Paginacao;

namespace APICatalogo.Models.Filtros;

public class CategoriasListarFiltro : ParametrosDaPaginacao
{
    public string? Nome { get; set; }
}
