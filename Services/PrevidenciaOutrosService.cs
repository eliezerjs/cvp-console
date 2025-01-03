using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaOutrosService : IPrevidenciaOutrosService
    {
        private const string PrevidenciaOutrosFolder = "PrevidenciaOutros";

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaOutrosService(IImportFilePrevConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaOutrosPdfAsync(Stream fileStream,  PrevidenciaOutrosType tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var PrevidenciaOutrosData = await _importFileConverterService.ProcessDataAsync(memoryStream);

            if (PrevidenciaOutrosData == null || !PrevidenciaOutrosData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");


            var primeiroParticipante = PrevidenciaOutrosData
                 .FirstOrDefault(e => e.ContainsKey("RecordType") && e["RecordType"] == "13");

            return GerarDocumentoPrevidenciaOutros(primeiroParticipante, tipo);
        }

        public byte[] GerarDocumentoPrevidenciaOutros(Dictionary<string, string> dados, PrevidenciaOutrosType tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaOutrosFolder);

            var campos = tipo switch
            {
                PrevidenciaOutrosType.PK11 => GetPK11(),
                PrevidenciaOutrosType.PK15 => GetPK15(),
                PrevidenciaOutrosType.PK35 => GetPK35(),
                PrevidenciaOutrosType.PK37 => GetPK37(),
                PrevidenciaOutrosType.PK44 => GetPK44(),
                PrevidenciaOutrosType.PK48 => GetPK48(),
                PrevidenciaOutrosType.PK49 => GetPK49(),
                PrevidenciaOutrosType.PK53 => GetPK53(),
                _ => throw new ArgumentException("Tipo de PrevidenciaOutros inválida.")
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
