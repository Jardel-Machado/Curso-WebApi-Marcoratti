using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Models.Filtros;
using APICatalogo.Paginacao;
using APICatalogo.Repositories.Interfaces;
using X.PagedList;

namespace APICatalogo.Repositories;

public class ProdutosRepository : GenericosRepository<Produto>, IProdutosRepository
{    

    public ProdutosRepository(AppDbContext context) : base(context)
    {        
    }

    public async Task<IPagedList<Produto>> BuscarProdutosPaginados(ParametrosDaPaginacao parametrosDaPaginacao)
    {
        var produtos = await BuscarTodos();
        
        var produtosOrdenados = produtos.OrderBy(x => x.ProdutoId).AsQueryable();

        //var produtosPaginados = PaginacaoConsulta<Produto>.ListaPaginada(produtosOrdenados, parametrosDaPaginacao.NumeroDaPagina, parametrosDaPaginacao.ItensPorPagina);

        var produtosPaginados = await produtosOrdenados.ToPagedListAsync(parametrosDaPaginacao.NumeroDaPagina, parametrosDaPaginacao.ItensPorPagina);

        return produtosPaginados;            
    }

    public async Task<IEnumerable<Produto>> BuscarProdutoPorCategorias(int id)
    {
        var produtos = await BuscarTodos();

        return produtos.Where(x => x.CategoriaId == id);
    }

    public async Task<IPagedList<Produto>> FiltrarProdutosPeloPreco(ProdutosListarFiltro produtosListarFiltro)
    {
        var produtos = await BuscarTodos();

        if(produtosListarFiltro.Preco.HasValue && !string.IsNullOrEmpty(produtosListarFiltro.PrecoCriterio))
        {
            if(produtosListarFiltro.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosListarFiltro.Preco.Value)
                                                                                    .OrderBy(p => p.Preco);
            }
            else if(produtosListarFiltro.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosListarFiltro.Preco.Value)
                                                                                    .OrderBy(p => p.Preco);
            }
            else if(produtosListarFiltro.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosListarFiltro.Preco.Value)
                                                                                    .OrderBy(p => p.Preco);
            }
        }

        //var produtosFiltrados = PaginacaoConsulta<Produto>.ListaPaginada(produtos.AsQueryable(),
        //                        produtosListarFiltro.NumeroDaPagina, produtosListarFiltro.ItensPorPagina);

        var produtosFiltrados = await produtos.ToPagedListAsync(produtosListarFiltro.NumeroDaPagina, produtosListarFiltro.ItensPorPagina);

        return produtosFiltrados;
    }
}
