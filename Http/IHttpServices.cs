namespace CVP.Routines.MotorArquivosComunicacao.Console.Http
{
    public interface IHttpServices
    {
        Task<string> GetSurveysListAsync();
    }
}