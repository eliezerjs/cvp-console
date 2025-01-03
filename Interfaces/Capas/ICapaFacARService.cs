using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas;

public interface ICapaFacARService
{
    byte[] GerarCapaFacAR(Dictionary<string, string> dadosBoleto, CapaFacARType tipo);

    Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  CapaFacARType tipo);
}
