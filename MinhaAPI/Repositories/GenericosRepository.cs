using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public class GenericosRepository<T> : IGenericosRepository<T> where T : class
{
    protected readonly AppDbContext context;

    public GenericosRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<T>> BuscarTodos()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> Buscar(Expression<Func<T, bool>> predicado)
    {
        return await context.Set<T>().FirstOrDefaultAsync(predicado);
    }

    public T Criar(T entidade)
    {
        context.Set<T>().Add(entidade);

        //context.SaveChanges();

        return entidade;
    }

    public T Editar(T entidade)
    {
        context.Set<T>().Update(entidade);

        //context.SaveChanges();

        return entidade;
    }

    public T Deletar(T entidade)
    {
        context.Set<T>().Remove(entidade);

        //context.SaveChanges();

        return entidade;
    }
}
