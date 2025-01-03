using CVP.Routines.MotorArquivosComunicacao.Console.Config;
using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using System.Data;
using System.Data.SqlClient;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Persistences
{
    public abstract class BasePersist
    {
        protected readonly IAppConfig AppConfig;
        protected RotinaConfiguration RotinaConfiguration => AppConfig.GetRotinaConfigurations();

        protected BasePersist(IAppConfig appConfig)
        {
            AppConfig = appConfig;
        }

        protected string ConnectionString => RotinaConfiguration.ConnectionStrings.DBApplication;

        protected void ExecutarComandoBanco(Action<IDbConnection> blocoExecucao)
        {
            using var connection = new SqlConnection(ConnectionString);
            blocoExecucao(connection);
        }
    }
}
