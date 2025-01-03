using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Const;
using CVP.Routines.MotorArquivosComunicacao.Enums;
using IntegraCVP.Application.Helper;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM4Service : IPrevidenciaM4Service
    {
        private const string PrevidenciaM4Folder = "PrevidenciaM4";

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaM4Service(IImportFilePrevConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream,  PrevidenciaM4Type tipo)
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

            var registrosValidos =  records.Where(record =>
                                    record.ContainsKey("BOLETO_TP_REGISTRO") && record["BOLETO_TP_REGISTRO"] == TipoLayout.PARTICIPANTE.ToString() &&
                                    record.ContainsKey("FUNDOS_TP_REGISTRO") && record["FUNDOS_TP_REGISTRO"] == TipoLayout.FUNDOS.ToString()
                                ).ToList();

            if (!registrosValidos.Any())
                throw new ArgumentException($"Nenhum registro válido encontrado para geração do PDF {tipo}.");

#if DEBUG
            // Em modo Debug, processa apenas o primeiro registro para facilitar o teste
            var firstRecord = records.FirstOrDefault();
            if (firstRecord == null)
                throw new InvalidOperationException("Nenhum registro encontrado para processar em modo Debug.");

            return new List<byte[]> { GerarDocumentoPrevidenciaM4(firstRecord, tipo) };
#else
                // Em modo Release, processa todos os registros
                return records.Select(record => GerarDocumentoPrevidenciaM1(record, tipo));
#endif            
        }

        private byte[] GerarDocumentoPrevidenciaM4(Dictionary<string, string> dados, PrevidenciaM4Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM4Folder);

            var campos = tipo switch
            {
                PrevidenciaM4Type.PK05 => GetPK05(),
                PrevidenciaM4Type.PK06 => GetPK06(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM4 inválida.")
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
