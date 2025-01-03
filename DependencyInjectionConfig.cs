using CVP.Routines.MotorArquivosComunicacao.Console.Config;
using CVP.Routines.MotorArquivosComunicacao.Console.Http;
using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.Console.Persistences;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.Console.Services.Capas;
using CVP.Routines.MotorArquivosComunicacao.Console.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CVP.Routines.MotorArquivosComunicacao.Console
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services
                .AddScoped<IAppConfig, AppConfig>()
                .AddScoped<IExecucaoRotinaPersist, ExecucaoRotinaPersist>()
                .AddScoped<ILogPersist, LogPersist>()
                .AddScoped<LoggingDelegatingHandler>()
                .AddScoped<ITerminalConverterService, TerminalConverterService>()
                .AddScoped<IImportFileConverterService, ImportFileConverterService>()
                .AddScoped<IBoletoM1Service, BoletoM1Service>()
                .AddScoped<IBoletoM2Service, BoletoM2Service>()
                .AddScoped<IBoletoM3Service, BoletoM3Service>()
                .AddScoped<IBoletoM4Service, BoletoM4Service>()
                .AddScoped<ICartaRecusaService, CartaRecusaService>()
                .AddScoped<IImportFilePrevConverterService, ImportFilePrevConverterService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IBoasVindasService, BoasVindasService>()
                .AddScoped<IPrestamistaService, PrestamistaService>()
                .AddScoped<IGeradorCodigoCepNetService, GeradorCodigoCepNetService>()
                .AddScoped<IPrevidenciaM1Service, PrevidenciaM1Service>()
                .AddScoped<IPrevidenciaM2Service, PrevidenciaM2Service>()
                .AddScoped<IPrevidenciaM3Service, PrevidenciaM3Service>()
                .AddScoped<IPrevidenciaM4Service, PrevidenciaM4Service>()
                .AddScoped<IPrevidenciaM5Service, PrevidenciaM5Service>()
                .AddScoped<IPrevidenciaM6Service, PrevidenciaM6Service>()
                .AddScoped<IPrevidenciaOutrosService, PrevidenciaOutrosService>()
                .AddScoped<ICapaFacSimplesColoridoService, CapaFacSimplesColoridoService>()
                .AddScoped<ICapaFacARService, CapaFacARService>()
                .AddScoped<ICapaFacSimplesPBService, CapaFacSimplesPBService>()
                .AddHttpClient<IHttpServices, HttpServices>(httpClient =>
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                })
                .AddHttpMessageHandler<LoggingDelegatingHandler>();
        }
    }
}
