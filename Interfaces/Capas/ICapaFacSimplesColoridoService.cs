using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas
{
    public interface ICapaFacSimplesColoridoService
    {
        byte[] GerarCapaFacSimplesColorido(Dictionary<string, string> dadosBoleto, CapaFacColoridoType tipo);

        Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  CapaFacColoridoType tipo);
    }
}
