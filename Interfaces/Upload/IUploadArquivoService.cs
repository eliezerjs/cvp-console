namespace CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Interfaces.Upload
{
    public interface IUploadArquivoService
    {
       /// <summary>
       ///  Método de upload de arquivos.
       /// </summary>
       /// <param name="filePath"></param>
       /// <param name="cpf"></param>
       /// <param name="idIdentificacao"></param>
       /// <param name="cookie"></param>
       /// <returns></returns>
        Task<string> UploadArquivoAsync(string filePath, string cpf, string idIdentificacao, string cookie);
    }
}
