namespace CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Interfaces.Upload
{
    public interface IAutenticacaoService
    {
        Task<string> ObterTokenAsync(string cpf, string funcao);
    }
}
