using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM5Service
    {        
        byte[] GerarDocumentoPrevidenciaM5(Dictionary<string, string> dadosBoleto, PrevidenciaM5Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM5PdfAsync(Stream fileStream,  PrevidenciaM5Type tipo);

    }
}
