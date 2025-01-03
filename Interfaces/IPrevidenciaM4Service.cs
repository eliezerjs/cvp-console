using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM4Service
    {       
        
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaM4PdfAsync(Stream fileStream,  PrevidenciaM4Type tipo);

    }
}
