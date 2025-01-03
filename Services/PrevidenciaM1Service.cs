using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Const;
using CVP.Routines.MotorArquivosComunicacao.Enums;

using IntegraCVP.Application.Helper;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM1Service : IPrevidenciaM1Service
    {
        private const string PrevidenciaM1Folder = "PrevidenciaM1";
        

        private readonly IImportFilePrevConverterService _importFilePrevConverterService;

        public PrevidenciaM1Service(IImportFilePrevConverterService dataConverterService)
        {
            _importFilePrevConverterService = dataConverterService;
        }

        public async Task<IEnumerable<byte[]>> ConverterEGerarPrevidenciaPdfAsync(Stream fileStream, PrevidenciaM1Type tipo)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            fileStream.Position = 0; 

            //if (!_importFilePrevConverterService.ValidarFormatoArquivo(fileStream))
            //{
            //    throw new ArgumentException("O formato do arquivo não é compatível.");
            //}
                        
            var records = await _importFilePrevConverterService.ProcessDataAsync(fileStream);

            if (records == null || !records.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            if (records == null || !records.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            #if DEBUG
                // Em modo Debug, processa apenas o primeiro registro para facilitar o teste
                var firstRecord = records.FirstOrDefault();
                if (firstRecord == null)
                    throw new InvalidOperationException("Nenhum registro encontrado para processar em modo Debug.");

                return new List<byte[]> { GerarDocumentoPrevidenciaM1(firstRecord, tipo) };
            #else
                // Em modo Release, processa todos os registros
                return records.Select(record => GerarDocumentoPrevidenciaM1(record, tipo));
            #endif
        }
        private byte[] GerarDocumentoPrevidenciaM1(Dictionary<string, string> dados, PrevidenciaM1Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM1Folder);

            var campos = tipo switch
            {
                PrevidenciaM1Type.PK28 => GetPK28(),
                PrevidenciaM1Type.PK29 => GetPK29(),
                PrevidenciaM1Type.PK30 => GetPK30(),
                PrevidenciaM1Type.PK31 => GetPK31(),
                PrevidenciaM1Type.PK32 => GetPK32(),
                PrevidenciaM1Type.PK33 => GetPK33(),
                PrevidenciaM1Type.PK34 => GetPK34(),
                PrevidenciaM1Type.PK36 => GetPK36(),
                PrevidenciaM1Type.PK47 => GetPK47(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM1 inválida.")
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
