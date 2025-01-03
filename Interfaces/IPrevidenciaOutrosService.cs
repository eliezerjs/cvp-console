using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaOutrosService
    {        
        byte[] GerarDocumentoPrevidenciaOutros(Dictionary<string, string> dadosBoleto, PrevidenciaOutrosType tipo);
        Task<byte[]> ConverterEGerarPrevidenciaOutrosPdfAsync(Stream fileStream,  PrevidenciaOutrosType tipo);

    }
}
