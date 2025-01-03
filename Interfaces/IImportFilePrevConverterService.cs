namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IImportFilePrevConverterService
    {
        bool ValidarFormatoArquivo(Stream fileStream);
        Task<string> ConverterArquivoParaJsonComProcessDataAsync(Stream fileStream);
        Task<List<Dictionary<string, string>>> ProcessDataAsync(Stream dataStream);
    }
}
