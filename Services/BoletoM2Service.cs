using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using System.Text.Json;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM2Service : IBoletoM2Service
    {
        public const string BoletoM2 = "BoletoM2";
        private readonly IImportFileConverterService _dataConverterService;

        public BoletoM2Service(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }

        public async Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM2Type tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var boletoData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (boletoData == null || !boletoData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var boletosFiltrados = boletoData
                .Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!boletosFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarBoletoM2(boletosFiltrados.FirstOrDefault(), tipo);
        }
        public byte[] GerarBoletoM2(Dictionary<string, string> dadosBoleto, BoletoM2Type tipo)
        {
            string imagePath = GetImagePath(tipo, BoletoM2);

            var campos = tipo switch
            {
                BoletoM2Type.VD32 => GetCamposVD32(),
                BoletoM2Type.VIDA26 => GetCamposVIDA26(),
                _ => throw new ArgumentException("Tipo de boleto inválido.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize, isBold) in campos)
            {
                document.AddTextField(dadosBoleto, key, x, y, fontSize, isBold, pdfPage);
            }

            if (dadosBoleto.TryGetValue("NUMCDBARRA", out var codigoDeBarras))
            {
                string especieMoeda = PdfHelper.ObterEspecieMoeda(
                    PdfHelper.ObterEspecieMoedaDoCodigoBarra(codigoDeBarras)
                );
                
                string codigoPadronizado = PdfHelper.MontarCodigoBarra(
                    codigoDeBarras,
                    PdfHelper.ObterFatorVencimento(dadosBoleto["VENCIMENT"]),
                    PdfHelper.ConverterValor(dadosBoleto["VALOR"])
                );

                document.AddBarcode(pdfDocument, codigoPadronizado, 50, 90);
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
