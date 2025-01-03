using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IEmailService
    {
        byte[] GerarDocumentoEmail(Dictionary<string, string> dadosBoleto, EmailType tipo);
        Task<byte[]> ConverterEGerarEmailPdfAsync(Stream fileStream,  EmailType tipo);
    }
}
