using APICatalogo.Models;
using System.Linq.Expressions;

namespace APICatalogo.Repositories.Interfaces;

public interface IGenericosRepository<T>
{
    Task<IEnumerable<T>> BuscarTodos();
    Task<T?> Buscar(Expression<Func<T, bool>> predicado);
    T Criar(T entidade);
    T Editar(T entidade);
    T Deletar(T entidade);
}
