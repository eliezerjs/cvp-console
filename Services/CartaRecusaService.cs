using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class CartaRecusaService : ICartaRecusaService
    {
        private const string CartaRecusaFolder = "CartaRecusa";

        private readonly IImportFileConverterService _dataConverterService;

        public CartaRecusaService(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarCartaRecusaPdfAsync(Stream fileStream,  CartaRecusaType tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var CartaRecusaData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (CartaRecusaData == null || !CartaRecusaData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var CartaRecusasFiltrados = CartaRecusaData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!CartaRecusasFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoCartaRecusa(CartaRecusasFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoCartaRecusa(Dictionary<string, string> dados, CartaRecusaType tipo)
        {
            string imagePath = GetImagePath(tipo, CartaRecusaFolder);

            var campos = tipo switch
            {
                CartaRecusaType.VIDA01 => GetVIDA01(),
                CartaRecusaType.VIDA02 => GetVIDA02(),
                CartaRecusaType.VIDA03 => GetVIDA03(),
                _ => throw new ArgumentException("Tipo de Carta de Recusa inválida.")
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
