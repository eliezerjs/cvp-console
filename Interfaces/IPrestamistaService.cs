using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrestamistaService
    {        
        byte[] GerarDocumentoPrestamista(Dictionary<string, string> dadosBoleto, PrestamistaType tipo);
        Task<byte[]> ConverterEGerarPrestamistaPdfAsync(Stream fileStream,  PrestamistaType tipo);

    }
}
