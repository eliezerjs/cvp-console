using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Entities
{

    public partial class LogServico
    {
        public long COD_LOG_SERVICO { get; set; }
        public long COD_EXECUCAO_ROTINA { get; set; }
        public string NOM_MODULO { get; set; }
        public string NOM_METODO { get; set; }
        public string DES_LOG { get; set; }
        public bool STA_ERRO { get; set; }
        public string DES_ERRO { get; set; }
        public string NOM_URL { get; set; }
        public string DES_ENVIO { get; set; }
        public string DES_RETORNO { get; set; }
        public DateTime DTH_LOG { get; set; } = DateTime.Now;

        public static LogServico Criar(string mensagem, string url = "", string envio = "", string retorno = "", Exception ex = null)
        {
            StackFrame frame = new(3);
            string fullName = frame.GetMethod().DeclaringType.FullName;

            Match classMatch = ClassRegex().Match(fullName);
            string className = classMatch.Success ? classMatch.Groups[1].Value : string.Empty;

            Match methodMatch = MethodRegex().Match(fullName);
            string methodName = methodMatch.Success ? methodMatch.Groups[1].Value : string.Empty;

            long codigoExecucaoRotina = RotinaConfiguration.IdentificadorExecucao;

            return new()
            {
                COD_EXECUCAO_ROTINA = codigoExecucaoRotina,
                DES_LOG = mensagem,
                DES_ERRO = ex?.StackTrace,
                NOM_METODO = methodName,
                NOM_MODULO = className,
                STA_ERRO = ex != null,
                NOM_URL = url,
                DES_ENVIO = envio,
                DES_RETORNO = retorno
            };
        }

        [GeneratedRegex(".*\\.(\\w+)\\+", RegexOptions.Compiled)]
        private static partial Regex ClassRegex();

        [GeneratedRegex("<(\\w+)>", RegexOptions.Compiled)]
        private static partial Regex MethodRegex();
    }
}