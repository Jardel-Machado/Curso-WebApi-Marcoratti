namespace APICatalogo.Paginacao;

public class PaginacaoConsulta<T> : List<T> where T : class
{
    public int PaginaAtual { get; set; }
    public int TotalDePaginas { get; set; }
    public int ItensPorPagina { get; set; }
    public int QuantidadeTotalDeItens { get; set; }

    public bool ExistePaginaAnterior => PaginaAtual > 1;
    public bool ExistePaginaSeguinte => PaginaAtual < TotalDePaginas;


    public PaginacaoConsulta(List<T> itens, int quantidadeDeItens, int numeroDaPagina, int itensPorPagina)
    {
        QuantidadeTotalDeItens = quantidadeDeItens;
        ItensPorPagina = itensPorPagina;
        PaginaAtual = numeroDaPagina;
        TotalDePaginas = (int)Math.Ceiling(quantidadeDeItens / (double)ItensPorPagina);

        AddRange(itens);
    }

    public static PaginacaoConsulta<T> ListaPaginada(IQueryable<T> dados, int numeroDaPagina, int itensPorPagina)
    {
        var quantidadeDeItens = dados.Count();

        var itens = dados.Skip((numeroDaPagina - 1) * itensPorPagina).Take(itensPorPagina).ToList();

        return new PaginacaoConsulta<T>(itens, quantidadeDeItens, numeroDaPagina, itensPorPagina);
    }
}
