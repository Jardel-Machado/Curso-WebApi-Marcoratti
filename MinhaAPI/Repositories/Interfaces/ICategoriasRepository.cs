using APICatalogo.Models;
using APICatalogo.Models.Filtros;
using APICatalogo.Paginacao;
using X.PagedList;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriasRepository : IGenericosRepository<Categoria>
{
    Task<IPagedList<Categoria>> BuscarCategoriasPaginadas(ParametrosDaPaginacao parametrosDaPaginacao);
    Task<IPagedList<Categoria>> FiltrarCategoriasPorNome(CategoriasListarFiltro categoriasListarFiltro);
    Task<IEnumerable<Categoria>> BuscarCategoriasPorProduto();
}
