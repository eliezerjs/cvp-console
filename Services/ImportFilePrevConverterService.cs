using System.Text;
using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class ImportFilePrevConverterService : IImportFilePrevConverterService
    {
        public bool ValidarFormatoArquivo(Stream fileStream)
        {
            if (fileStream == null || !fileStream.CanRead)
                throw new ArgumentException("O stream é inválido ou não pode ser lido.");

            fileStream.Position = 0; // Reposiciona o stream no início
            using var reader = new StreamReader(fileStream, Encoding.GetEncoding("ISO-8859-1"));

            string line;
            int lineNumber = 0;

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

                if (lineNumber == 1) 
                {
                    if (line.Length < 10 || !DateTime.TryParse(line.Substring(0, 10), out _))
                    {
                        return false;
                    }
                }
                else if (lineNumber > 1)
                {
                    if (line.Length < 2 || string.IsNullOrWhiteSpace(line.Substring(0, 2)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<string> ConverterArquivoParaJsonComProcessDataAsync(Stream fileStream)
        {
            if (fileStream == null || !fileStream.CanRead)
                throw new ArgumentException("O stream é inválido ou não pode ser lido.");

            fileStream.Position = 0; // Garante que o stream começa do início

            // Usa o ProcessDataAsync para processar os dados do arquivo
            var registrosProcessados = await ProcessDataAsync(fileStream);

            if (registrosProcessados == null || !registrosProcessados.Any())
                throw new InvalidOperationException("Nenhum dado válido foi encontrado no arquivo.");

            // Converte a lista de dicionários para JSON
            return JsonSerializer.Serialize(registrosProcessados, new JsonSerializerOptions { WriteIndented = true });
        }

        public async Task<List<Dictionary<string, string>>> ProcessDataAsync(Stream dataStream)
        {
            var resultadoAgrupado = new Dictionary<string, Dictionary<string, string>>();

            using (var reader = new StreamReader(dataStream, Encoding.GetEncoding("ISO-8859-1")))
            {
                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Length < 2)
                        continue;

                    string recordType = line.Substring(0, 2);

                    if (recordType == "00" || recordType == "12")
                        continue;

                    if (Layouts.TryGetValue(recordType, out var layout))
                    {
                        var record = new Dictionary<string, string>();

                        foreach (var field in layout)
                        {
                            if (line.Length >= field.Offset + field.Size - 1)
                            {
                                record[field.Name] = line.Substring(field.Offset - 1, field.Size).Trim();
                            }
                            else
                            {
                                record[field.Name] = string.Empty;
                            }
                        }

                        record["RecordType"] = recordType;

                        // Extrair o valor de NO_CERTIFICADO com base na posição e tamanho definidos
                        string noCertificado = line.Length >= 3 + 15 - 1
                            ? line.Substring(3 - 1, 15).Trim()
                            : string.Empty;

                        if (!string.IsNullOrWhiteSpace(noCertificado))
                        {
                            if (!resultadoAgrupado.ContainsKey(noCertificado))
                            {
                                resultadoAgrupado[noCertificado] = new Dictionary<string, string>();
                            }

                            foreach (var campo in record)
                            {
                                if (!resultadoAgrupado[noCertificado].ContainsKey(campo.Key))
                                {
                                    resultadoAgrupado[noCertificado][campo.Key] = campo.Value;
                                }
                            }
                        }
                    }
                }
            }

            return resultadoAgrupado.Values.ToList();
        }
    }
}
