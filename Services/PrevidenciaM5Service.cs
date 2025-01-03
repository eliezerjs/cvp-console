using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM5Service : IPrevidenciaM5Service
    {
        private const string PrevidenciaM5Folder = "PrevidenciaM5";

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaM5Service(IImportFilePrevConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM5PdfAsync(Stream fileStream,  PrevidenciaM5Type tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var PrevidenciaM5Data = await _importFileConverterService.ProcessDataAsync(memoryStream);

            if (PrevidenciaM5Data == null || !PrevidenciaM5Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

           
                var registro = PrevidenciaM5Data
                .FirstOrDefault(e => e.ContainsKey("RecordType") && e["RecordType"] == "26"); 

                return GerarDocumentoPrevidenciaM5(registro, tipo);
           

            return null;
        }

        public byte[] GerarDocumentoPrevidenciaM5(Dictionary<string, string> dados, PrevidenciaM5Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM5Folder);

            var campos = tipo switch
            {
                PrevidenciaM5Type.PK12 => GetPK12(),
                PrevidenciaM5Type.PK13 => GetPK13(),
                PrevidenciaM5Type.PK14 => GetPK14(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM5 inválida.")
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
