using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IInadimplenciaService
    {
        byte[] GerarDocumentoInadimplencia(Dictionary<string, string> dadosBoleto, InadimplenciaType tipo);
        Task<byte[]> ConverterEGerarInadimplenciaPdfAsync(Stream fileStream,  InadimplenciaType tipo);
    }
}
