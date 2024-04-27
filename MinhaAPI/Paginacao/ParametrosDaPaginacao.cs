namespace APICatalogo.Paginacao;

public class ParametrosDaPaginacao
{
    const int tamanhoMaximoDeItensPorPagina = 100;
    public int NumeroDaPagina { get; set; } = 1;
    private int _itensPorPagina = tamanhoMaximoDeItensPorPagina;
    public int ItensPorPagina
    {
        get
        {
            return _itensPorPagina;
        }
        set
        {
            _itensPorPagina = ( value > tamanhoMaximoDeItensPorPagina) ? tamanhoMaximoDeItensPorPagina : value;
        }
    }

}
