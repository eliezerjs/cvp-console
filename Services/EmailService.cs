using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class EmailService : IEmailService
    {
        private const string EmailFolder = "Email";

        private readonly IImportFileConverterService _dataConverterService;

        public EmailService(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarEmailPdfAsync(Stream fileStream,  EmailType tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var emailData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (emailData == null || !emailData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var emailsFiltrados = emailData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!emailsFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoEmail(emailsFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoEmail(Dictionary<string, string> dados, EmailType tipo)
        {
            string imagePath = GetImagePath(tipo, EmailFolder);

            var campos = tipo switch
            {              
                EmailType.VIDA18 => GetCamposVIDA18(),
                EmailType.VD08 => GetCamposVD08(),
                EmailType.VD09 => GetCamposVD09(),
                EmailType.VIDA17 => GetCamposVIDA17(),
                _ => throw new ArgumentException("Tipo de email inválido.")
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
