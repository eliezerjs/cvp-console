using System.Text.Json;

namespace CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Helpers
{
    public static class TokenHelper
    {
        public static string ProcessTokenResponse(string responseBody)
        {
            if (string.IsNullOrWhiteSpace(responseBody))
                throw new ArgumentException("A resposta não pode ser nula ou vazia.", nameof(responseBody));

            // Desserializar o JSON
            var responseJson = JsonSerializer.Deserialize<TokenResponse>(responseBody);
            if (responseJson?.Dados == null)
                throw new InvalidOperationException("Resposta inválida ou dados ausentes.");

            string mapData = responseJson.Dados.Mapdata;
            string keyData = responseJson.Dados.KeyData;

            if (string.IsNullOrWhiteSpace(mapData) || string.IsNullOrWhiteSpace(keyData))
                throw new InvalidOperationException("Mapdata ou KeyData ausentes na resposta.");

            // Processar os dados para gerar rsaShaKey
            return GenerateRsaShaKey(mapData, keyData);
        }

        private static string GenerateRsaShaKey(string mapData, string keyData)
        {
            string dataret = string.Empty;

            for (int g = 0; g < keyData.Length; g += 4)
            {
                int position = int.Parse(keyData.Substring(g, 4));
                dataret += mapData[position];
            }

            return dataret;
        }

        // Modelo da resposta JSON
        private class TokenResponse
        {
            public TokenData Dados { get; set; }
        }

        private class TokenData
        {
            public string Mapdata { get; set; }
            public string KeyData { get; set; }
        }
    }
}
