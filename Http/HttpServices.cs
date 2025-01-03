using CVP.Routines.MotorArquivosComunicacao.Console.Config;
using CVP.Routines.MotorArquivosComunicacao.Console.Entities;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Http
{
    public class HttpServices : IHttpServices
    {
        private readonly HttpClient _httpClient;
        private readonly RotinaConfiguration _rotinaConfiguration;

        public HttpServices(
            IAppConfig appConfig,
            HttpClient httpClient)
        {
            _rotinaConfiguration = appConfig.GetRotinaConfigurations();

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_rotinaConfiguration.Credencials.BaseURL);
        }

        public async Task<string> GetSurveysListAsync()
        {
            string urlService = string.Format(UrlService.GetSurveys, _rotinaConfiguration.Credencials.User);

            var request = new HttpRequestMessage(HttpMethod.Get, urlService);
            request.Headers.Add("Authorization", $"Bearer {_rotinaConfiguration.Credencials.Token}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}