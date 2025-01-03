using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IPrevidenciaM2Service
    {
        Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaM2PdfAsync(Stream fileStream, PrevidenciaM2Type tipo);

    }
}
