using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers; 

namespace CVP.Routines.MotorArquivosComunicacao.Console.FileProcessingRoutine
{
    class Program
    {
        private static readonly string InputPath = Environment.GetEnvironmentVariable("INPUT_PATH") ?? throw new InvalidOperationException("Variable INPUT_PATH is not set.");
        private static readonly string OutputPath = Environment.GetEnvironmentVariable("OUTPUT_PATH") ?? throw new InvalidOperationException("Variable OUTPUT_PATH is not set.");

        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Starting file processing routine...");

            // Validate directories
            if (!Directory.Exists(InputPath))
            {
                throw new DirectoryNotFoundException($"Input path '{InputPath}' does not exist.");
            }

            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
                System.Console.WriteLine($"Output path '{OutputPath}' created.");
            }

            System.Timers.Timer timer = new System.Timers.Timer(60000); 
            timer.Elapsed += async (sender, e) => await ProcessFilesAsync();
            timer.Start();

            System.Console.WriteLine("Routine is running. Press Enter to exit.");
            System.Console.ReadLine();
        }

        private static async Task ProcessFilesAsync()
        {
            try
            {
                System.Console.WriteLine("Checking for files...");
                var files = Directory.GetFiles(InputPath);

                foreach (var file in files)
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        System.Console.WriteLine($"Processing file: {fileName}");

                        // Convert file to PDF (dummy conversion for demonstration)
                        byte[] pdfData = await ConvertToPdfAsync(File.ReadAllBytes(file));

                        string outputFilePath = Path.Combine(OutputPath, $"{fileName}.pdf");
                        await File.WriteAllBytesAsync(outputFilePath, pdfData);

                        System.Console.WriteLine($"File '{fileName}' converted to PDF and saved to '{OutputPath}'");

                        // Delete the original file
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine($"Error processing file '{file}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Routine encountered an error: {ex.Message}");
            }
        }

        private static Task<byte[]> ConvertToPdfAsync(byte[] fileData)
        {
            // Simulate PDF conversion (replace with real logic)
            return Task.FromResult(fileData);
        }
    }
}
