using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CVP.Routines.MotorArquivosComunicacao.ProcessData
{
    public class ProcessDataService<TService, TEnum>
        where TService : class
        where TEnum : Enum
    {
        private readonly TService _service;

        public ProcessDataService(TService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task ProcessFilesAsync(
            string inputPath,
            string outputPath,
            string processedFilesPath,
            TEnum tipo,
            Func<TService, Stream, TEnum, Task<IEnumerable<byte[]>>> converterMethod,
            Func<Stream, Task<string>> jsonGeneratorMethod = null,
            string jsonOutputPath = null)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("O caminho de entrada é inválido.", nameof(inputPath));

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("O caminho de saída é inválido.", nameof(outputPath));

            //if (string.IsNullOrWhiteSpace(processedFilesPath))
            //    throw new ArgumentException("O caminho dos arquivos processados é inválido.", nameof(processedFilesPath));

            if (!Directory.Exists(inputPath))
                throw new DirectoryNotFoundException($"O diretório de entrada '{inputPath}' não foi encontrado.");

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            if (!Directory.Exists(processedFilesPath))
                Directory.CreateDirectory(processedFilesPath);

            if (jsonGeneratorMethod != null && string.IsNullOrWhiteSpace(jsonOutputPath))
                throw new ArgumentException("O caminho de saída para os arquivos JSON é inválido.", nameof(jsonOutputPath));

            if (jsonGeneratorMethod != null && !Directory.Exists(jsonOutputPath))
                Directory.CreateDirectory(jsonOutputPath);

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

                    if (jsonGeneratorMethod != null)
                    {
                        fileStream.Position = 0; 
                        var jsonContent = await jsonGeneratorMethod(fileStream);
                        var jsonFileName = Path.Combine(jsonOutputPath, $"{Path.GetFileNameWithoutExtension(file)}-{tipo}.json");
                        await File.WriteAllTextAsync(jsonFileName, jsonContent);
                        System.Console.WriteLine($"Arquivo JSON gerado: {jsonFileName}");
                    }

                    fileStream.Position = 0;
                    IEnumerable<byte[]> pdfsData = await converterMethod(_service, fileStream, tipo);
                    int pdfIndex = 1;

                    foreach (var pdf in pdfsData)
                    {
                        string outputFileName = Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(file)}-{tipo}-{DateTime.Now:dd-MM-yyyy-HH-mm-ss}-{pdfIndex}.pdf");
                        await File.WriteAllBytesAsync(outputFileName, pdf);

                        pdfIndex++;
                        System.Console.WriteLine($"Arquivo processado e salvo como PDF: {outputFileName}");
                    }
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
