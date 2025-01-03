using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface ICartaRecusaService
    {
        byte[] GerarDocumentoCartaRecusa(Dictionary<string, string> dadosBoleto, CartaRecusaType tipo);
        Task<byte[]> ConverterEGerarCartaRecusaPdfAsync(Stream fileStream,  CartaRecusaType tipo);
    }
}
