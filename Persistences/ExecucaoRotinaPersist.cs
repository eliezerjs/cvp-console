using CVP.Routines.MotorArquivosComunicacao.Console.Config;
using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;
using Dapper;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Persistences
{
    public class ExecucaoRotinaPersist(
      IAppConfig appConfig) : BasePersist(appConfig), IExecucaoRotinaPersist
    {
        public bool AtualizarExecucao(long codigoExecucaoRotina, bool ocorreuErro)
        {
            const string Script = @"UPDATE
                                        DBO.PS_001_EXECUCAO_ROTINA
                                    SET
                                        DTH_FIM_EXECUCAO_ROTINA = GETDATE(),
                                        STA_ERRO = @STA_ERRO
                                    WHERE
                                        COD_EXECUCAO_ROTINA = @COD_EXECUCAO_ROTINA";

            int linhasAfetadas = 0;

            ExecutarComandoBanco(connection =>
            {
                linhasAfetadas = connection.Execute(Script, new
                {
                    COD_EXECUCAO_ROTINA = codigoExecucaoRotina,
                    STA_ERRO = ocorreuErro
                });
            });

            return linhasAfetadas > 0;
        }

        public long GravarExecucao()
        {
            const string Script = @"INSERT INTO DBO.PS_001_EXECUCAO_ROTINA (
                                      DES_HOSTNAME_SERVIDOR, DTH_INICIO_EXECUCAO_ROTINA, STA_ERRO,
                                      DTH_FIM_EXECUCAO_ROTINA
                                    )
                                    VALUES
                                    (
                                      @DES_HOSTNAME_SERVIDOR, @DTH_INICIO_EXECUCAO_ROTINA, @STA_ERRO,
                                      @DTH_FIM_EXECUCAO_ROTINA
                                    );
                                    SELECT SCOPE_IDENTITY();";

            ExecucaoRotina execucaoRotina = ExecucaoRotina.Criar();

            int codigoExecucao = 0;

            ExecutarComandoBanco(connection =>
            {
                codigoExecucao = connection.QueryFirst<int>(Script, execucaoRotina);
            });

            return codigoExecucao;
        }
    }
}