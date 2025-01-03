using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Const;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM2Service : IPrevidenciaM2Service
    {
        private const string PrevidenciaM2Folder = "PrevidenciaM2";

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaM2Service(IImportFilePrevConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaM2PdfAsync(Stream fileStream,  PrevidenciaM2Type tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var records = await _importFileConverterService.ProcessDataAsync(memoryStream);

            //valida os dados necessários para gerar o pdf.
            var possuiAmbosTipos = records.Any(r => r.ContainsKey("PRODUTO_TP_REGISTRO") && r["PRODUTO_TP_REGISTRO"] == "04") &&
                                   records.Any(r => r.ContainsKey("PARTICIPANTE_TP_REGISTRO") && r["PARTICIPANTE_TP_REGISTRO"] == "03");

            if (records == null || !records.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            if (!possuiAmbosTipos)
            {
                throw new ArgumentException("O arquivo não contém dados necessários para geração do pdf.");
            }
            
            return records.Select(record => GerarDocumentoPrevidenciaM2(record, tipo));
        }

        private byte[] GerarDocumentoPrevidenciaM2(Dictionary<string, string> dados, PrevidenciaM2Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM2Folder);

            var campos = tipo switch
            {
                PrevidenciaM2Type.PK08 => GetPK08(),
                PrevidenciaM2Type.PK09 => GetPK09(),
                PrevidenciaM2Type.PK10 => GetPK10(),                
                _ => throw new ArgumentException("Tipo de PrevidenciaM2 inválida.")
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
