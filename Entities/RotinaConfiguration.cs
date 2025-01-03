namespace CVP.Routines.MotorArquivosComunicacao.Console.Entities
{
    public class RotinaConfiguration
    {
        public static long IdentificadorExecucao { get; set; }
        public ConnectionStringsOptions ConnectionStrings { get; set; }
        public CredencialsOptions Credencials { get; set; }
    }

    public class ConnectionStringsOptions
    {
        public string DBApplication { get; set; }
    }

    public class CredencialsOptions
    {
        public string User { get; set; }
        public string Token { get; set; }
        public string BaseURL { get; set; }
    }
}