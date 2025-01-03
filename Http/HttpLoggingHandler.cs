using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Http
{
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly string[] CamposAutenticacaoRequisicao = ["sharsakey"];
        private readonly string[] CamposAutenticacaoResposta = ["mapdata", "keydata"];
        private readonly ILogPersist _logPersist;

        public LoggingDelegatingHandler(ILogPersist logPersist)
        {
            _logPersist = logPersist;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestContent = "", responseContent = "";
            Exception exception = null;

            try
            {
                requestContent = await request.Content.ReadAsStringAsync(cancellationToken);
                requestContent = FiltrarDadosSensiveis(requestContent, CamposAutenticacaoRequisicao);

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                responseContent = response.Content != null ? await response.Content.ReadAsStringAsync(cancellationToken) : "";
                responseContent = FiltrarDadosSensiveis(responseContent, CamposAutenticacaoResposta);

                return response;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                string serviceName = request.RequestUri.Segments.LastOrDefault();

                if (exception == null)
                {
                    _logPersist.LogarInformacao($"Requisitando serviço: {serviceName}", request.RequestUri.AbsoluteUri, requestContent, responseContent);
                }
                else
                {
                    _logPersist.LogarErro($"Requisitando serviço: {serviceName}", request.RequestUri.AbsoluteUri, requestContent, responseContent, exception);
                }
            }
        }

        private static string FiltrarDadosSensiveis(string jsonContent, string[] propertiesToReplace)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonContent))
                    return jsonContent;

                using var jsonDoc = JsonDocument.Parse(jsonContent);
                var root = jsonDoc.RootElement.Clone();

                bool isResponse = jsonContent.Contains("sucesso", StringComparison.CurrentCultureIgnoreCase);

                var obj = new JsonObject();
                foreach (var element in root.EnumerateObject())
                {
                    obj[element.Name] = DeveSubstituir(element.Name, propertiesToReplace)
                        ? JsonValue.Create("ValorRemovido")
                        : ProcessarElemento(element, propertiesToReplace, isResponse);
                }

                return obj.ToString();
            }
            catch
            {
                return jsonContent;
            }
        }

        private static bool DeveSubstituir(string propertyName, string[] propertiesToReplace)
        {
            return propertiesToReplace.Contains(propertyName.ToLower());
        }

        private static JsonNode ProcessarElemento(JsonProperty element, string[] propertiesToReplace, bool isResponse)
        {
            return isResponse && element.Name == "dados"
                ? ProcessarDados(element.Value, propertiesToReplace)
                : JsonNode.Parse(element.Value.GetRawText())!;
        }

        private static JsonObject ProcessarDados(JsonElement dados, string[] propertiesToReplace)
        {
            var dadosObj = new JsonObject();
            foreach (var subElement in dados.EnumerateObject())
            {
                dadosObj[subElement.Name] = DeveSubstituir(subElement.Name, propertiesToReplace)
                    ? JsonValue.Create("ValorRemovido")
                    : JsonNode.Parse(subElement.Value.GetRawText());
            }
            return dadosObj;
        }
    }
}