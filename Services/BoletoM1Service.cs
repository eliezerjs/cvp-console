using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM1Service : IBoletoM1Service
    {
        public const string BoletoM1 = "BoletoM1";

        private readonly IImportFileConverterService _importFileConverterService;

        public BoletoM1Service(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }

        public async Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM1Type tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var boletoData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (boletoData == null || !boletoData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var boletosFiltrados = boletoData
                .Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!boletosFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarBoletoM1(boletosFiltrados.FirstOrDefault(), tipo);
        }
        public byte[] GerarBoletoM1(Dictionary<string, string> dadosBoleto, BoletoM1Type tipo)
        {
            string imagePath = GetImagePath(tipo, BoletoM1);
            
            var campos = tipo switch
            {
                BoletoM1Type.VD02 => GetCamposVD02(),
                BoletoM1Type.VIDA25 => GetCamposVIDA25(),                
                _ => throw new ArgumentException("Tipo de boleto inválido.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize, isBold) in campos)
            {
                document.AddTextField(dadosBoleto, key, x, y, fontSize, isBold,  pdfPage);
            }

            if (dadosBoleto.TryGetValue("NUMCDBARRA", out var codigoDeBarras))
            {
                string especieMoeda = PdfHelper.ObterEspecieMoeda(
                    PdfHelper.ObterEspecieMoedaDoCodigoBarra(codigoDeBarras)
                );
                document.AddTextField(especieMoeda, 262, 585, 8, false, pdfPage);

                string codigoPadronizado = PdfHelper.MontarCodigoBarra(
                    codigoDeBarras,
                    PdfHelper.ObterFatorVencimento(dadosBoleto["VENCIMENT"]),
                    PdfHelper.ConverterValor(dadosBoleto["VALOR"])
                );

                document.AddBarcode(pdfDocument, codigoPadronizado, 50, 77);
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
