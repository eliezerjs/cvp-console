

namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface ITerminalConverterService
    {
        Task<byte[]> ConverterEGerarZipAsync(Stream fileStream);
        Task<string> ConverterEGerarPrevidenciaAsync(Stream fileStream);
    }
}
