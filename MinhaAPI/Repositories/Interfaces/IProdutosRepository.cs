using APICatalogo.Models;
using APICatalogo.Models.Filtros;
using APICatalogo.Paginacao;
using X.PagedList;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository : IGenericosRepository<Produto>
{
    Task<IPagedList<Produto>> BuscarProdutosPaginados(ParametrosDaPaginacao parametrosDaPaginacao);
    Task<IPagedList<Produto>> FiltrarProdutosPeloPreco(ProdutosListarFiltro produtosListarFiltro);
    Task<IEnumerable<Produto>> BuscarProdutoPorCategorias(int id);
}
