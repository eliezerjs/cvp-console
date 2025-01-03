using CVP.Routines.MotorArquivosComunicacao.Enums;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services.Capas;

public class CapaFacSimplesPBService : ICapaFacSimplesPBService
{
    public Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  CapaFacPBType tipo)
    {
        throw new NotImplementedException();
    }

    public byte[] GerarCapaFacSimplesPB(Dictionary<string, string> dadosBoleto, CapaFacPBType tipo)
    {
        throw new NotImplementedException();
    }
}
