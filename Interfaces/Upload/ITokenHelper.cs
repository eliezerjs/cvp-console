namespace CVP.Routines.MotorArquivosComunicacao.ConsoleApp.Interfaces.Upload
{
    public interface ITokenHelper
    {
        Task<TokenData> GetTokenDataAsync();
    }

    public class TokenData
    {
        public string UserName { get; set; }
        public string ShaRsaKey { get; set; }
    }
}
