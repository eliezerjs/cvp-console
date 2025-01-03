using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM6Service
    {
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream,  PrevidenciaM6Type tipo);

    }
}
