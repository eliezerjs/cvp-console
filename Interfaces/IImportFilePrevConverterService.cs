namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IImportFilePrevConverterService
    {
        public bool ValidarFormatoArquivo(Stream fileStream);
        Task<List<Dictionary<string, string>>> ProcessDataAsync(Stream dataStream);
    }
}
