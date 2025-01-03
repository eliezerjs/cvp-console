using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas;

public interface ICapaFacSimplesPBService
{
    byte[] GerarCapaFacSimplesPB(Dictionary<string, string> dadosBoleto, CapaFacPBType tipo);

    Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  CapaFacPBType tipo);
}
