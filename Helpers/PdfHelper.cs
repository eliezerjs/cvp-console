using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCVP.Application.Helper
{
    using iText.Kernel.Pdf;
    using iText.Layout;
    using iText.Kernel.Geom;
    using iText.Layout.Element;
    using iText.IO.Image;
    using System.IO;
    using System.Collections.Generic;
    using iText.Barcodes;
    using System.Text.RegularExpressions;
    using iText.Kernel.Colors;
    using iText.Kernel.Font;
    using System.Runtime.CompilerServices;

    public static class PdfHelper
    {
        public static (Document document, PdfDocument pdfDocument, PdfPage pdfPage) InitializePdfDocument(string imagePath, MemoryStream pdfStream)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
            }

            var writer = new PdfWriter(pdfStream);
            var pdfDocument = new PdfDocument(writer);
            var pdfPage = pdfDocument.AddNewPage(PageSize.A4);
            var document = new Document(pdfDocument);

            byte[] imageBytes = File.ReadAllBytes(imagePath);
            var imageData = ImageDataFactory.Create(imageBytes);
            var image = new Image(imageData);
            image.ScaleToFit(pdfPage.GetPageSize().GetWidth(), pdfPage.GetPageSize().GetHeight());
            image.SetFixedPosition(0, 0);
            document.Add(image);

            return (document, pdfDocument, pdfPage);
        }

        public static void AddTextField(this Document document, string text, float x, float y, float fontSize, bool isBold, PdfPage pdfPage)
        {
            float width = text.Length * fontSize * 0.9f;

            PdfFont font = PdfFontFactory.CreateFont(isBold ? "Helvetica-Bold" : "Helvetica", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

            if (width > pdfPage.GetPageSize().GetWidth() - x)
            {
                width = pdfPage.GetPageSize().GetWidth() - x;
            }

            if (!string.IsNullOrEmpty(text))
            {
                var paragraph = new Paragraph(text)
                    .SetFont(font)
                    .SetFontSize(fontSize)                   
                    .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, width);
                document.Add(paragraph);
            }
        }

        public static void AddTextField(this Document document, string text, float x, float y, float fontSize, PdfPage pdfPage, bool isBold)
        {
            float width = text.Length * fontSize * 0.9f;

            PdfFont font = PdfFontFactory.CreateFont(isBold ? "Helvetica-Bold" : "Helvetica", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

            if (width > pdfPage.GetPageSize().GetWidth() - x)
            {
                width = pdfPage.GetPageSize().GetWidth() - x;
            }

            if (!string.IsNullOrEmpty(text))
            {
                //Color color = fontColor.ToLower() switch
                //{
                //    "Black" => ColorConstants.BLACK,
                //    "white" => ColorConstants.WHITE,
                //    "red" => ColorConstants.RED,
                //    "blue" => ColorConstants.BLUE,
                //    "green" => ColorConstants.GREEN,
                //    _ => ColorConstants.BLACK
                //};

                var paragraph = new Paragraph(text)
                    .SetFontSize(fontSize)
                    //.SetFontColor(color)
                    .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, width);
                document.Add(paragraph);
            }
        }


        public static void AddTextField(this Document document, Dictionary<string, string> fields, string key, float x, float y, float fontSize, bool isBold, PdfPage pdfPage)
        {
            if (fields.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value))
            {
                document.AddTextField(value, x, y, fontSize, isBold, pdfPage);
            }
        }

        public static void AddBarcode(this Document document, PdfDocument pdfDocument, string code, float x, float y, float barHeight = 30, float barWidth = 1f)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("O código de barras não pode ser nulo ou vazio.");
            }

            var barcode = new Barcode128(pdfDocument);
            barcode.SetCode(code);
            barcode.SetBarHeight(barHeight);
            barcode.SetX(barWidth);

            var barcodeImage = new Image(barcode.CreateFormXObject(pdfDocument));
            barcodeImage.SetFixedPosition(x, y);
            document.Add(barcodeImage);
        }

        public static byte[] GerarPdf(Dictionary<string, string> dados, string imagePath, List<(string Key, float X, float Y, float FontSize, bool isBold)> campos)
        {
            using var pdfStream = new MemoryStream();

            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize, isBold) in campos)
            {
                document.AddTextField(dados, key, x, y, fontSize, isBold, pdfPage);
            }

            document.Close();
            return pdfStream.ToArray();
        }

        public static string MontarCodigoBarra(string codigoExpandido, string fatorVencimento, string valorBoleto)
        {
            string codigoLimpo = Regex.Replace(codigoExpandido, @"\D", "");

            if (codigoLimpo.Length < 25)
                throw new InvalidOperationException("O código expandido é inválido.");

            string bancoMoeda = codigoLimpo.Substring(0, 4);

            string livreUso = codigoLimpo.Substring(4, 25);

            string codigoBase = $"{bancoMoeda}0{fatorVencimento}{valorBoleto.PadLeft(10, '0')}{livreUso}";

            return CalcularDigitoVerificador(codigoBase);
        }

        public static string CalcularDigitoVerificador(string codigo)
        {
            int peso = 2;
            int soma = 0;

            for (int i = codigo.Length - 1; i >= 0; i--)
            {
                int num = int.Parse(codigo[i].ToString());
                soma += num * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }

            int resto = soma % 11;
            int dv = resto == 0 || resto == 1 ? 1 : 11 - resto;

            return codigo.Substring(0, 4) + dv + codigo.Substring(5);
        }

        public static string ObterFatorVencimento(string dataVencimento)
        {
            DateTime baseDate = new DateTime(1997, 10, 7);
            DateTime vencimento = DateTime.Parse(dataVencimento);
            return ((vencimento - baseDate).Days).ToString().PadLeft(4, '0');
        }

        public static string ConverterValor(string valor)
        {
            string valorLimpo = Regex.Replace(valor, @"[.,]", "");
            return valorLimpo.PadLeft(10, '0');
        }

        public static string ObterEspecieMoedaDoCodigoBarra(string codigoBarra)
        {
            if (string.IsNullOrEmpty(codigoBarra) || codigoBarra.Length < 4)
            {
                throw new ArgumentException("Código de barras inválido ou vazio.");
            }

            // A quarta posição indica a espécie de moeda
            return codigoBarra[3].ToString();
        }

        public static string ObterEspecieMoeda(string codigoMoeda)
        {
            var especiesMoeda = new Dictionary<string, string>
            {
                { "9", "BRL" }, // Real Brasileiro
                { "1", "USD" }, // Dólar Americano
                { "2", "EUR" }, // Euro
                { "3", "ARS" }, // Peso Argentino
                { "4", "GBP" }, // Libra Esterlina
                { "5", "CLP" }, // Peso Chileno
                { "6", "JPY" }, // Iene Japonês
                { "7", "CAD" }, // Dólar Canadense
                { "8", "AUD" }, // Dólar Australiano
            };

            return especiesMoeda.TryGetValue(codigoMoeda, out var sigla)
                ? sigla
                : "UNK";
        }

        public static string CalcularCodigoCedente(Dictionary<string, string> dadosBoleto)
        {
            if (!dadosBoleto.ContainsKey("AGENCIA") || string.IsNullOrWhiteSpace(dadosBoleto["AGENCIA"]))
                throw new ArgumentException("O campo 'AGENCIA' é obrigatório para calcular o Código Cedente.");

            if (!dadosBoleto.ContainsKey("NUMCDBARRA") || string.IsNullOrWhiteSpace(dadosBoleto["NUMCDBARRA"]))
                throw new ArgumentException("O campo 'NUMCDBARRA' é obrigatório para calcular o Código Cedente.");

            string agencia = dadosBoleto["AGENCIA"].Trim();
            string numCdBarra = dadosBoleto["NUMCDBARRA"].Replace(" ", "").Trim();
            string nossoNumero = numCdBarra.Substring(0, 6);

            string codigoCedente = $"{agencia}-{nossoNumero}";

            return codigoCedente;
        }
    }
}
