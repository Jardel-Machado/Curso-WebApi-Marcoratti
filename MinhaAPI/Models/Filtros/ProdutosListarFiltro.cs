using APICatalogo.Paginacao;

namespace APICatalogo.Models.Filtros;

public class ProdutosListarFiltro : ParametrosDaPaginacao
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; }
}
