using CVP.Routines.MotorArquivosComunicacao.Enums;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;

namespace CVP.Routines.MotorArquivosComunicacao.ProcessData
{
    public class ProcessDataPrevidenciaM2
    {
        private readonly IPrevidenciaM2Service _previdenciaM2Service;

        public ProcessDataPrevidenciaM2(IPrevidenciaM2Service previdenciaM2Service)
        {
            _previdenciaM2Service = previdenciaM2Service ?? throw new ArgumentNullException(nameof(previdenciaM2Service));
        }

        public async Task ProcessFilesAsync(string inputPath, string outputPath, string processedFilesPath, PrevidenciaM2Type tipo)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("O caminho de entrada é inválido.", nameof(inputPath));

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("O caminho de saída é inválido.", nameof(outputPath));

            if (string.IsNullOrWhiteSpace(processedFilesPath))
                throw new ArgumentException("O caminho dos arquivos processados é inválido.", nameof(processedFilesPath));

            if (!Directory.Exists(inputPath))
                throw new DirectoryNotFoundException($"O diretório de entrada '{inputPath}' não foi encontrado.");

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            if (!Directory.Exists(processedFilesPath))
                Directory.CreateDirectory(processedFilesPath);

            var files = Directory.GetFiles(inputPath, "*.txt");

            if (!files.Any())
            {
                System.Console.WriteLine("Nenhum arquivo encontrado para processamento.");
                return;
            }

            foreach (var file in files)
            {
                FileStream fileStream = null;
                try
                {
                    System.Console.WriteLine($"Processando arquivo: {file}");

                    fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

                    if (!fileStream.CanRead)
                        throw new ArgumentException($"O arquivo '{file}' não pode ser lido.");

                    IEnumerable<byte[]> pdfsData = await _previdenciaM2Service.ConverterEGerarPrevidenciaM2PdfAsync(fileStream, tipo);
                    int pdfIndex = 1;

                    foreach (var pdf in pdfsData)
                    {
                        string outputFileName = Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(file)}-{tipo}-{DateTime.Now:dd-MM-yyyy-HH-mm-ss}-{pdfIndex}.pdf");
                        await File.WriteAllBytesAsync(outputFileName, pdf);

                        pdfIndex++;
                        System.Console.WriteLine($"Arquivo processado e salvo como PDF: {outputFileName}");
                    }

                    //string processedFileName = Path.Combine(processedFilesPath, Path.GetFileName(file));
                    //File.Move(file, processedFileName);

                    //System.Console.WriteLine($"Arquivo movido para a pasta de arquivos processados: {processedFileName}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Erro ao processar o arquivo {file}: {ex.Message}");
                }
                finally
                {
                    fileStream?.Dispose();
                }
            }
        }
    }
}
