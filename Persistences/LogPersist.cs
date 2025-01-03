using CVP.Routines.MotorArquivosComunicacao.Console.Config;
using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;
using Dapper;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Persistences
{
    public class LogPersist(
  IAppConfig appConfig) : BasePersist(appConfig), ILogPersist
    {
        private bool Logar(string mensagem, string url = "", string jsonEnvio = "", string jsonRetorno = "", Exception exception = null)
        {
            const string Script = @"INSERT INTO DBO.PS_002_LOG_ROTINA (
                                      COD_EXECUCAO_ROTINA, NOM_MODULO,
                                      NOM_METODO, DES_LOG, STA_ERRO, DES_ERRO,
                                      NOM_URL, DES_ENVIO, DES_RETORNO, DTH_LOG
                                    )
                                    VALUES
                                      (
                                        @COD_EXECUCAO_ROTINA, @NOM_MODULO,
                                        @NOM_METODO, @DES_LOG, @STA_ERRO, @DES_ERRO,
                                        @NOM_URL, @DES_ENVIO, @DES_RETORNO, @DTH_LOG
                                      )
                                    ";

            LogServico logServico = LogServico.Criar(mensagem, url, jsonEnvio, jsonRetorno, exception);

            int linhasAfetadas = 0;

            ExecutarComandoBanco(connection =>
            {
                linhasAfetadas = connection.Execute(Script, logServico);
            });

            return linhasAfetadas > 0;
        }

        public bool LogarInformacao(string mensagem, string url, string jsonEnvio, string jsonRetorno) => Logar(mensagem, url, jsonEnvio, jsonRetorno);

        public bool LogarInformacao(string mensagem) => Logar(mensagem);

        public bool LogarErro(string mensagem, Exception ex) => Logar(mensagem, exception: ex);

        public bool LogarErro(string mensagem, string url, string jsonEnvio, string jsonRetorno, Exception ex) => Logar(mensagem, url, jsonEnvio, jsonRetorno, ex);
    }
}
