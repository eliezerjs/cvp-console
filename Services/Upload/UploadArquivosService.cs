using System.Text;
using System.Text.Json;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Helpers;
using CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Interfaces.Upload;

namespace CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Services.Upload
{
    public class UploadArquivoService : IUploadArquivoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _uploadUrl;
        private readonly ITokenHelper _tokenHelper;

        public UploadArquivoService(HttpClient httpClient, string uploadUrl, ITokenHelper tokenHelper)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _uploadUrl = uploadUrl ?? throw new ArgumentNullException(nameof(uploadUrl));
            _tokenHelper = tokenHelper ?? throw new ArgumentNullException(nameof(tokenHelper));
        }

        public async Task<string> UploadArquivoAsync(string filePath, string cpf, string idIdentificacao, string cookie)
        {
            ValidarParametros(filePath, cpf, idIdentificacao);

            // Obtenção de dados necessários via TokenHelper
            var tokenData = await _tokenHelper.GetTokenDataAsync();

            if (string.IsNullOrWhiteSpace(tokenData.UserName) || string.IsNullOrWhiteSpace(tokenData.ShaRsaKey))
                throw new InvalidOperationException("Os dados obtidos pelo TokenHelper são inválidos.");

            using var formContent = CriarConteudoForm(filePath, tokenData.UserName, tokenData.ShaRsaKey, cpf, idIdentificacao);

            var request = CriarRequisicao(formContent, cookie);

            var response = await _httpClient.SendAsync(request);

            return await ProcessarResposta(response);
        }

        private void ValidarParametros(string filePath, string cpf, string idIdentificacao)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("O caminho do arquivo não pode ser nulo ou vazio.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"O arquivo '{filePath}' não foi encontrado.");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("O campo 'cpf' é obrigatório.", nameof(cpf));

            if (string.IsNullOrWhiteSpace(idIdentificacao))
                throw new ArgumentException("O campo 'idIdentificacao' é obrigatório.", nameof(idIdentificacao));
        }

        private MultipartFormDataContent CriarConteudoForm(string filePath, string userName, string shaRsaKey, string cpf, string idIdentificacao)
        {
            var formContent = new MultipartFormDataContent();

            using var fileStream = File.OpenRead(filePath);
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            formContent.Add(fileContent, "Arquivo", Path.GetFileName(filePath));

            var paramJson = JsonSerializer.Serialize(new
            {
                UserName = userName,
                SHArsaKey = shaRsaKey,
                cpf = cpf,
                IdIdentificacao = idIdentificacao
            });
            formContent.Add(new StringContent(paramJson, Encoding.UTF8, "application/json"), "param");

            return formContent;
        }

        private HttpRequestMessage CriarRequisicao(MultipartFormDataContent formContent, string cookie)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _uploadUrl)
            {
                Content = formContent
            };

            request.Headers.Add("Cookie", cookie);

            return request;
        }

        private async Task<string> ProcessarResposta(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Falha no upload. Status: {response.StatusCode}, Mensagem: {await response.Content.ReadAsStringAsync()}");

            var responseContent = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);

            if (responseData?.Sucesso != true || responseData.Dados?.Caminho == null)
                throw new InvalidOperationException("Erro ao processar a resposta da API.");

            return responseData.Dados.Caminho;
        }

        private class ApiResponse
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public DadosResponse Dados { get; set; }
        }

        private class DadosResponse
        {
            public string Caminho { get; set; }
        }
    }
}
