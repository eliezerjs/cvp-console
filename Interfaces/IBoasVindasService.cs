using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IBoasVindasService
    {
        byte[] GerarDocumentoBoasVindas(Dictionary<string, string> dadosBoleto, BoasVindasType tipo);
        Task<byte[]> ConverterEGerarBoasVindasPdfAsync(Stream fileStream, BoasVindasType tipo);
    }
}
