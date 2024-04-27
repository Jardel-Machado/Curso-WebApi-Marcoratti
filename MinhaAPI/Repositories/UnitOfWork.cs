using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{    
    private IProdutosRepository? produtosRepo;
    private ICategoriasRepository? categoriasRepo;
    public AppDbContext context;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    public IProdutosRepository ProdutosRepository
    {
        get 
        {
            return produtosRepo = produtosRepo ?? new ProdutosRepository(context);
        }
    }
    public ICategoriasRepository CategoriasRepository
    {
        get 
        {
            return categoriasRepo = categoriasRepo ?? new CategoriasRepository(context);
        }
    }

    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
