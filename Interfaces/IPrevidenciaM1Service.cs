using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM1Service
    {        
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaM1PdfAsync(Stream fileStream, PrevidenciaM1Type tipo);
    }
}
