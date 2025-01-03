using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using System.Text.RegularExpressions;
using iText.Barcodes;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;
using CVP.Routines.MotorArquivosComunicacao.Enums;

using System.Text.Json;
using IntegraCVP.Application.Helper;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrestamistaService : IPrestamistaService
    {
        private const string InadimplenciaFolder = "Prestamista";

        private readonly IImportFileConverterService _importFileConverterService;
        public PrestamistaService(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrestamistaPdfAsync(Stream fileStream,  PrestamistaType tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var InadimplenciaData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (InadimplenciaData == null || !InadimplenciaData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var InadimplenciasFiltrados = InadimplenciaData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!InadimplenciasFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrestamista(InadimplenciasFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrestamista(Dictionary<string, string> dados, PrestamistaType tipo)
        {
            string imagePath = GetImagePath(tipo, InadimplenciaFolder);

            var campos = tipo switch
            {
                PrestamistaType.PREST01 => GetCamposPrest01(),
                _ => throw new ArgumentException("Tipo de Inadimplencia inválida.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize, isBold) in campos)
            {
                if (dados.ContainsKey(key))
                {
                    document.AddTextField(dados[key], x, y, fontSize, isBold, pdfPage);
                }
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
