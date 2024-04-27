namespace APICatalogo.Repositories.Interfaces;

public interface IUnitOfWork
{
    IProdutosRepository ProdutosRepository { get; }
    ICategoriasRepository CategoriasRepository { get; }
    Task Commit();
}
