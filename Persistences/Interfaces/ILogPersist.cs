namespace CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces
{
    public interface ILogPersist
    {
        bool LogarInformacao(string mensagem);
        bool LogarInformacao(string mensagem, string url, string jsonEnvio, string jsonRetorno);
        bool LogarErro(string mensagem, Exception ex);
        bool LogarErro(string mensagem, string url, string jsonEnvio, string jsonRetorno, Exception ex);
    }
}
