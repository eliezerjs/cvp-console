using CVP.Routines.MotorArquivosComunicacao.Enums;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services.Capas;

public class CapaFacARService : ICapaFacARService
{
    public Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  CapaFacARType tipo)
    {
        throw new NotImplementedException();
    }

    public byte[] GerarCapaFacAR(Dictionary<string, string> dadosBoleto, CapaFacARType tipo)
    {
        throw new NotImplementedException();
    }
}
