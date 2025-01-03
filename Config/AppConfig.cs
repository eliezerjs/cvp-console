using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using Microsoft.Extensions.Configuration;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Config
{
    public class AppConfig : IAppConfig
    {
        public IConfiguration GetConfigurations() =>
            new ConfigurationBuilder()
#if !DEBUG
            .AddJsonFile($"appsettings.json")
#else
            .AddJsonFile($"appsettings.Development.json")
#endif
            .Build();

        public RotinaConfiguration GetRotinaConfigurations()
        {
            RotinaConfiguration configOptions = new();
            GetConfigurations().Bind(configOptions);
            return configOptions;
        }
    }
}