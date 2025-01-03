using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using Newtonsoft.Json;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public class ImportFileConverterService : IImportFileConverterService
    {
        public string ConvertFileToJson(Stream fileStream)
        {
            var registros = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(fileStream))
            {
                string line;
                int maxFieldLength = 20; // Comprimento máximo do campo, ajustável conforme necessário
                int lineCounter = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue; // Ignora linhas em branco
                    }

                    var registro = ProcessLine(line, maxFieldLength, lineCounter);
                    registros.Add(registro);
                    lineCounter++;
                }
            }

            // Converte a lista de registros para JSON
            return JsonConvert.SerializeObject(registros, Formatting.Indented);
        }

        private Dictionary<string, string> ProcessLine(string line, int maxFieldLength, int lineNumber)
        {
            var registro = new Dictionary<string, string>();
            int startIndex = 0;
            int fieldCounter = 1;

            // Percorre a linha, extraindo campos com base no comprimento máximo
            while (startIndex < line.Length)
            {
                int fieldLength = DetermineFieldLength(line, startIndex, maxFieldLength);
                string fieldValue = line.Substring(startIndex, fieldLength).Trim();

                registro[$"Campo{fieldCounter}"] = fieldValue;

                startIndex += fieldLength;
                fieldCounter++;
            }

            // Adiciona o número da linha (opcional, para rastrear a origem)
            registro["LineNumber"] = lineNumber.ToString();

            return registro;
        }

        private int DetermineFieldLength(string line, int startIndex, int maxFieldLength)
        {
            // Analisa o segmento para determinar o comprimento do campo
            int remainingLength = line.Length - startIndex;
            int length = Math.Min(maxFieldLength, remainingLength);

            // Caso haja um delimitador claro (como espaço), podemos ajustar aqui
            for (int i = startIndex; i < startIndex + length; i++)
            {
                if (char.IsWhiteSpace(line[i]))
                {
                    return i - startIndex + 1;
                }
            }

            return length;
        }
        public string ConvertToJson(Stream fileStream)
        {
            var sections = new List<Dictionary<string, string>>();
            Dictionary<string, string> currentSection = null;
            List<string> headers = null;
            string tipoDadoAtual = null;

            using var reader = new StreamReader(fileStream);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // Ignora linhas irrelevantes
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("%%") || line.StartsWith("%!"))
                {
                    continue;
                }

                // Detecta o TIPO_DADO
                if ((line.Contains(" - ") && line.ToUpper().Contains("BOLETO"))
                    || line.Contains("VIDA01") || line.Contains("VIDA03") || line.Contains("VIDA04")
                    || line.Contains("VIDA02") || line.Contains("VD33") || line.Contains("VIDA27")
                    || line.Contains("VIDA05") || line.Contains("VD08") || line.Contains("VIDA07")
                    || line.Contains("VD09") || line.Contains("VIDA17") || line.Contains("VIDA18"))
                {
                    tipoDadoAtual = line.Split(" - ", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
                }
                else if (line.Contains("STARTDBM")) // Início de uma nova seção
                {
                    if (currentSection != null)
                    {
                        sections.Add(currentSection);
                    }

                    currentSection = new Dictionary<string, string>
            {
                { "TIPO_DADO", tipoDadoAtual }
            };

                    headers = null; // Reinicia os cabeçalhos
                }
                else if (line.Contains("|") && headers == null) // Captura os cabeçalhos
                {
                    headers = line.Split('|', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(header => header.Trim())
                                  .ToList();
                }
                else if (currentSection != null && headers != null && line.Contains("|"))
                {
                    // Processa as linhas de dados
                    var values = line.Split('|', StringSplitOptions.None);

                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (i < values.Length)
                        {
                            var key = headers[i].Trim();
                            var value = values[i].Trim();

                            if (!string.IsNullOrEmpty(key) && !value.StartsWith("(") && !value.StartsWith(")") && value != "SETDBSEP")
                            {
                                currentSection[key] = value;
                            }
                        }
                    }
                }
            }

            if (currentSection != null)
            {
                sections.Add(currentSection);
            }

            return JsonConvert.SerializeObject(sections, Formatting.Indented);
        }
    }
}
