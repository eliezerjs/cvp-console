using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM5Service
    {   
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream,  PrevidenciaM5Type tipo);

    }
}
