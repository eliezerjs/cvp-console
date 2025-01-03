using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoasVindasService : IBoasVindasService
    {
        private const string BoasVindasFolder = "BoasVindas";

        private readonly IImportFileConverterService _dataConverterService;

        public BoasVindasService(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }

        public async Task<byte[]> ConverterEGerarBoasVindasPdfAsync(Stream fileStream, BoasVindasType tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            fileStream.Position = 0; 

            var jsonResult = _dataConverterService.ConvertToJson(fileStream);

            var BoasVindasData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (BoasVindasData == null || !BoasVindasData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var BoasVindasFiltrados = BoasVindasData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!BoasVindasFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoBoasVindas(BoasVindasFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoBoasVindas(Dictionary<string, string> dados, BoasVindasType tipo)
        {
            string imagePath = GetImagePath(tipo, BoasVindasFolder);

            var campos = tipo switch
            {
                BoasVindasType.VIDA05 => GetCamposVIDA05(),
                BoasVindasType.VIDA07 => GetCamposVIDA07(),
                _ => throw new ArgumentException("Tipo de Boas-Vindas inválido.")
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
