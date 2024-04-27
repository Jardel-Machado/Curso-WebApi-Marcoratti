using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Models.Filtros;
using APICatalogo.Paginacao;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace APICatalogo.Repositories;

public class CategoriasRepository : GenericosRepository<Categoria>, ICategoriasRepository
{   

    public CategoriasRepository(AppDbContext context) : base(context)
    {
        
    }
   
    public async Task<IEnumerable<Categoria>> BuscarCategoriasPorProduto()
    {
        return await context.Categorias.Include(x => x.Produtos).ToListAsync();
    }

    public async Task<IPagedList<Categoria>> BuscarCategoriasPaginadas(ParametrosDaPaginacao parametrosDaPaginacao)
    {
        var categorias = await BuscarTodos();
        
        var categoriasOrdenadas = categorias.OrderBy(x => x.CategoriaId).AsQueryable();

        //var categoriasPaginados = PaginacaoConsulta<Categoria>.ListaPaginada(categoriasOrdenadas, parametrosDaPaginacao.NumeroDaPagina, parametrosDaPaginacao.ItensPorPagina);

        var categoriasPaginados = await categorias.ToPagedListAsync(parametrosDaPaginacao.NumeroDaPagina, parametrosDaPaginacao.ItensPorPagina);

        return categoriasPaginados;
    }

    public async Task<IPagedList<Categoria>> FiltrarCategoriasPorNome(CategoriasListarFiltro categoriasListarFiltro)
    {
        var categorias = await BuscarTodos();        

        if(!string.IsNullOrWhiteSpace(categoriasListarFiltro.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasListarFiltro.Nome));
        }

        //var categoriasFiltradas = PaginacaoConsulta<Categoria>.ListaPaginada(categorias.AsQueryable(),
        //                        categoriasListarFiltro.NumeroDaPagina, categoriasListarFiltro.ItensPorPagina);

        var categoriasFiltradas = await categorias.ToPagedListAsync(categoriasListarFiltro.NumeroDaPagina, categoriasListarFiltro.ItensPorPagina);

        return categoriasFiltradas;
    }
}
