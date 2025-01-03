using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM4Service : IBoletoM4Service
    {
        public const string BoletoM4 = "BoletoM4";

        private readonly IImportFileConverterService _dataConverterService;

        public BoletoM4Service(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM4Type tipo)
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

            return this.GerarBoletoM4(boletosFiltrados.FirstOrDefault(), tipo);
        }
        public byte[] GerarBoletoM4(Dictionary<string, string> dadosBoleto, BoletoM4Type tipo)
        {
            string imagePath = GetImagePath(tipo, BoletoM4);

            var campos = tipo switch
            {
                BoletoM4Type.VA18 => GetCamposVA18(),
                BoletoM4Type.VA24 => GetCamposVA24(),
                BoletoM4Type.VIDA23 => GetCamposVIDA23(),
                BoletoM4Type.VIDA24 => GetCamposVIDA24(),
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
                string codigoPadronizado = PdfHelper.MontarCodigoBarra(
                    codigoDeBarras,
                    PdfHelper.ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
                    PdfHelper.ConverterValor(dadosBoleto["VALOR"])
                );

                if (tipo == BoletoM4Type.VA18)
                {
                    document.AddBarcode(pdfDocument, codigoPadronizado, 50, 130);
                }
                else if (tipo == BoletoM4Type.VA24)
                {
                    document.AddBarcode(pdfDocument, codigoPadronizado, 50, 168);
                } else if (tipo == BoletoM4Type.VIDA23)
                {
                    document.AddBarcode(pdfDocument, codigoPadronizado, 50, 145);
                } else if (tipo == BoletoM4Type.VIDA24)
                {
                    document.AddBarcode(pdfDocument, codigoPadronizado, 50, 73);
                }

            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
