using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Const;


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
        public async Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream,  PrevidenciaOutrosType tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var records = await _importFileConverterService.ProcessDataAsync(memoryStream);

            if (records == null || !records.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            if (records == null || !records.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var permissao = records.Any(r => r.ContainsKey("BOLETO_TP_REGISTRO") && r["BOLETO_TP_REGISTRO"] == TipoLayout.BOLETO);

            if (records == null || !records.Any())
                throw new ArgumentException($"O arquivo não contém dados válidos para a geração do pdf {tipo}.");

            if (!permissao)
            {
                throw new ArgumentException($"O arquivo não contém dados necessários para geração do pdf {tipo}.");
            }

#if DEBUG
            var firstRecord = records.FirstOrDefault();
            if (firstRecord == null)
                throw new InvalidOperationException("Nenhum registro encontrado para processar em modo Debug.");

            return new List<byte[]> { GerarDocumentoPrevidenciaOutros(firstRecord, tipo) };
#else
                // Em modo Release, processa todos os registros
                return records.Select(record => GerarDocumentoPrevidenciaM1(record, tipo));
#endif 
            return records.Select(record => GerarDocumentoPrevidenciaOutros(record, tipo));
        }

        private byte[] GerarDocumentoPrevidenciaOutros(Dictionary<string, string> dados, PrevidenciaOutrosType tipo)
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
