
namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        this.loggerName = loggerName;
        this.loggerConfig = loggerConfig;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem) 
    {
        string caminhoArquivoLog = @"C:\Users\dell_\OneDrive\Área de Trabalho\Curso Web API ASP DOTNET Core Essencial - Marcoratti\log\arquivo_log.txt";

        using(StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true)) 
        {
            try
            {
                streamWriter.Write(mensagem);
                streamWriter.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
