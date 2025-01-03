using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM3Service
    {   
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaM3PdfAsync(Stream fileStream,  PrevidenciaM3Type tipo);
    }
}
