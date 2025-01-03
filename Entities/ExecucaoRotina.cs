using System.Net;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Entities
{
    public class ExecucaoRotina
    {
        public long COD_EXECUCAO_ROTINA { get; set; }
        public string DES_HOSTNAME_SERVIDOR { get; set; }
        public bool STA_ERRO { get; set; }
        public DateTime DTH_INICIO_EXECUCAO_ROTINA { get; set; }
        public DateTime? DTH_FIM_EXECUCAO_ROTINA { get; set; }

        public static ExecucaoRotina Criar()
        {
            string hostName = Dns.GetHostName();

            return new()
            {
                DES_HOSTNAME_SERVIDOR = hostName,
                STA_ERRO = false,
                DTH_INICIO_EXECUCAO_ROTINA = DateTime.Now
            };
        }
    }
}