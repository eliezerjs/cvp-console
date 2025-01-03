using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM6Service
    {        
        byte[] GerarDocumentoPrevidenciaM6(Dictionary<string, string> dadosBoleto, PrevidenciaM6Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM6PdfAsync(Stream fileStream,  PrevidenciaM6Type tipo);

    }
}
