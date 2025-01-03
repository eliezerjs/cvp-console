using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Const;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM3Service : IPrevidenciaM3Service
    {
        private const string PrevidenciaM3Folder = "PrevidenciaM3";

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaM3Service(IImportFilePrevConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream,  PrevidenciaM3Type tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var records = await _importFileConverterService.ProcessDataAsync(memoryStream);

            var permissao = records.Any(r => r.ContainsKey("COBERTURA_PROTECAO_TP_REGISTRO") && r["COBERTURA_PROTECAO_TP_REGISTRO"] == TipoLayout.COBERTURA_PROTECAO);
            
            if (records == null || !records.Any())
                throw new ArgumentException($"O arquivo não contém dados válidos para a geração do pdf {tipo}.");

            if (!permissao)
            {
                throw new ArgumentException($"O arquivo não contém dados necessários para geração do pdf {tipo}.");
            }

#if DEBUG
            // Em modo Debug, processa apenas o primeiro registro para facilitar o teste
            var firstRecord = records.FirstOrDefault();
            if (firstRecord == null)
                throw new InvalidOperationException("Nenhum registro encontrado para processar em modo Debug.");

            return new List<byte[]> { GerarDocumentoPrevidenciaM3(firstRecord, tipo) };
#else
                // Em modo Release, processa todos os registros
                return records.Select(record => GerarDocumentoPrevidenciaM1(record, tipo));
#endif            
        }

        private byte[] GerarDocumentoPrevidenciaM3(Dictionary<string, string> dados, PrevidenciaM3Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM3Folder);

            var campos = tipo switch
            {
                PrevidenciaM3Type.PK56 => GetPK56(),
                PrevidenciaM3Type.PK57 => GetPK57(),
                PrevidenciaM3Type.PK58 => GetPK58(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM3 inválida.")
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
