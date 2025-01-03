using System.IO.Compression;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using Newtonsoft.Json;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public class TerminalConverterService : ITerminalConverterService
    {
        private readonly IImportFileConverterService _dataConverterService;
        private readonly IBoletoM1Service _boletoM1Service;
        private readonly IBoletoM4Service _boletoM4Service;
        private readonly IPrestamistaService _prestamistaService;

        public TerminalConverterService(
            IImportFileConverterService dataConverterService,
            IBoletoM1Service boletoM1Service,
            IBoletoM4Service boletoM4Service,
            IPrestamistaService prestamistaService)
        {
            _dataConverterService = dataConverterService ?? throw new ArgumentNullException(nameof(dataConverterService));
            _boletoM1Service = boletoM1Service ?? throw new ArgumentNullException(nameof(boletoM1Service));
            _boletoM4Service = boletoM4Service ?? throw new ArgumentNullException(nameof(boletoM4Service));
            _prestamistaService = prestamistaService ?? throw new ArgumentNullException(nameof(prestamistaService));
        }

        public async Task<byte[]> ConverterEGerarZipAsync(Stream fileStream)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            fileStream.Position = 0;

            // Converte o arquivo para JSON
            var jsonResult = _dataConverterService.ConvertToJson(fileStream);
            var boletoData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonResult);

            if (boletoData == null || !boletoData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var pdfFiles = new List<(string FileName, byte[] Data)>();

            // Processa os boletos M1 e M4
            ProcessarBoletosM1PorTipo(boletoData, BoletoM1Type.VD02, pdfFiles);
            ProcessarBoletosM1PorTipo(boletoData, BoletoM1Type.VIDA25, pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VA18, pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VA24, pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VIDA23, pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VIDA24, pdfFiles);

            // Gera o arquivo ZIP
            return GerarZipComPdfs(pdfFiles);
        }

        public async Task<string> ConverterEGerarPrevidenciaAsync(Stream fileStream)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            fileStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(fileStream);
            return jsonResult;
        }

        private void ProcessarBoletosM1PorTipo(
            List<Dictionary<string, string>> boletos,
            BoletoM1Type tipo,
            List<(string FileName, byte[] Data)> pdfFiles)
        {
            var boletosFiltrados = boletos
                .Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString())
                .ToList();

            foreach (var itemBoleto in boletosFiltrados)
            {
                var pdfData = _boletoM1Service.GerarBoletoM1(itemBoleto, tipo);
                var fileName = $"{tipo}_{itemBoleto.GetValueOrDefault("FATURA", "Unknown")}.pdf";
                pdfFiles.Add((fileName, pdfData));
            }
        }

        private void ProcessarBoletosM4PorTipo(
            List<Dictionary<string, string>> boletos,
            BoletoM4Type tipo,
            List<(string FileName, byte[] Data)> pdfFiles)
        {
            var boletosFiltrados = boletos
                .Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString())
                .ToList();

            foreach (var itemBoleto in boletosFiltrados)
            {
                var pdfData = _boletoM4Service.GerarBoletoM4(itemBoleto, tipo);
                var fileName = $"{tipo}_{itemBoleto.GetValueOrDefault("NUMDOCTO", "Unknown")}.pdf";
                pdfFiles.Add((fileName, pdfData));
            }
        }

        private byte[] GerarZipComPdfs(List<(string FileName, byte[] Data)> pdfFiles)
        {
            using var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in pdfFiles)
                {
                    var zipEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                    using var entryStream = zipEntry.Open();
                    entryStream.Write(file.Data, 0, file.Data.Length);
                }
            }

            return zipStream.ToArray();
        }
    }
}
