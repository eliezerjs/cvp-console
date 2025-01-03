using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using Microsoft.Extensions.Configuration;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Config
{
    public interface IAppConfig
    {
        public IConfiguration GetConfigurations();
        public RotinaConfiguration GetRotinaConfigurations();
    }
}
