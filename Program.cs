using CVP.Routines.MotorArquivosComunicacao.Enums;
using CVP.Routines.MotorArquivosComunicacao.ProcessData;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;
using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.Console;

// Configuração de variáveis de ambiente
Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "");

// Configuração de dependências
ServiceCollection sc = new();
DependencyInjectionConfig.Configure(sc);
ServiceProvider sp = sc.BuildServiceProvider();

// Variáveis de ambiente
int interval = int.TryParse(Environment.GetEnvironmentVariable("TIMER_INTERVAL"), out var result) ? result : 60000;
string inputPath = Environment.GetEnvironmentVariable("INPUT_PATH") ?? throw new InvalidOperationException("INPUT_PATH is not set.");
string outputPath = Environment.GetEnvironmentVariable("OUTPUT_PATH") ?? throw new InvalidOperationException("OUTPUT_PATH is not set.");
string processedFilesPath = Environment.GetEnvironmentVariable("PROCESSED_FILES_PATH") ?? throw new InvalidOperationException("PROCESSED_FILES_PATH is not set.");

System.Timers.Timer timer = new(interval);

//timer.Elapsed += async (sender, e) =>
//{
    try
    {
        // Lista de serviços e tipos
        var servicesAndTypes = new List<(object Service, Type EnumType)>
        {
            (sp.GetService<IPrevidenciaM1Service>(), typeof(PrevidenciaM1Type)),
            (sp.GetService<IPrevidenciaM2Service>(), typeof(PrevidenciaM2Type)),
            (sp.GetService<IPrevidenciaM3Service>(), typeof(PrevidenciaM3Type)),
            (sp.GetService<IPrevidenciaM4Service>(), typeof(PrevidenciaM4Type)),
            (sp.GetService<IPrevidenciaM5Service>(), typeof(PrevidenciaM5Type)),
            (sp.GetService<IPrevidenciaM6Service>(), typeof(PrevidenciaM6Type)),
            (sp.GetService<IPrevidenciaOutrosService>(), typeof(PrevidenciaOutrosType))
        };

        foreach (var (service, enumType) in servicesAndTypes)
        {
            foreach (var enumValue in Enum.GetValues(enumType))
            {
                var processData = new ProcessDataService<object, Enum>(service);

                // Conversão dinâmica do método de conversão
                Func<object, Stream, Enum, Task<IEnumerable<byte[]>>> converterMethod = (svc, stream, tipo) =>
                    (Task<IEnumerable<byte[]>>)svc.GetType()
                        .GetMethod("ConverterEGerarPrevidenciaPdfAsync")?
                        .Invoke(svc, new object[] { stream, tipo });

                await processData.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, (Enum)enumValue, converterMethod);
            }
        }

        Console.WriteLine("Todos os arquivos foram processados com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao processar arquivos: {ex.Message}");
    }
//};

timer.Start();

Console.WriteLine("Rotina iniciada. Pressione Enter para encerrar.");
Console.ReadLine();

timer.Stop();
timer.Dispose();

async Task ExecutarRotina(Func<Task> blocoExecucao)
{
    bool ocorreuErro = false;
    var logPersist = sp.GetService<ILogPersist>();
    var execucaoRotinaPersist = sp.GetService<IExecucaoRotinaPersist>();

    try
    {
        RotinaConfiguration.IdentificadorExecucao = execucaoRotinaPersist.GravarExecucao();
        logPersist.LogarInformacao("Iniciando execução da rotina.");
        await blocoExecucao();
    }
    catch (Exception ex)
    {
        ocorreuErro = true;
        logPersist.LogarErro(ex.Message, ex);
    }
    finally
    {
        logPersist.LogarInformacao("Finalizando execução da rotina.");
        execucaoRotinaPersist.AtualizarExecucao(RotinaConfiguration.IdentificadorExecucao, ocorreuErro);
    }
}
