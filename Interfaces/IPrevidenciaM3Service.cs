using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM3Service
    {   
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream,  PrevidenciaM3Type tipo);
    }
}
