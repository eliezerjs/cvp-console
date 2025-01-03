using System.Text;
using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Interfaces.Upload;
using Microsoft.Extensions.Configuration;

namespace CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Services.Upload
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _autenticacaoUrl;

        public AutenticacaoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _autenticacaoUrl = configuration["ApiSettings:AutenticacaoUrl"]
                ?? throw new InvalidOperationException("A URL de autenticação não está configurada.");
        }

        public async Task<string> ObterTokenAsync(string cpf, string funcao)
        {
            var requestBody = new
            {
                cpf,
                funcao
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(_autenticacaoUrl, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erro ao autenticar. Status code: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter token de autenticação.", ex);
            }
        }
    }
}
