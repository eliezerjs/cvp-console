using CVP.Routines.MotorArquivosComunicacao.Console;
using CVP.Routines.MotorArquivosComunicacao.Console.Entities;
using CVP.Routines.MotorArquivosComunicacao.Console.Http;
using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;
using CVP.Routines.MotorArquivosComunicacao.ProcessData;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;

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

System.Timers.Timer timer = new System.Timers.Timer(interval);




timer.Elapsed += async (sender, e) =>
{
    try
    {
        /*
        var processDataM1 = new ProcessDataPrevidenciaM1(sp.GetService<IPrevidenciaM1Service>());

        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK28);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK29);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK30);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK31);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK32);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK33);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK34);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK36);
        await processDataM1.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM1Type.PK47);

        var processDataM2 = new ProcessDataPrevidenciaM2(sp.GetService<IPrevidenciaM2Service>());

        await processDataM2.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM2Type.PK08);
        await processDataM2.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM2Type.PK09);
        await processDataM2.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM2Type.PK10);
        */
        var processDataM3 = new ProcessDataPrevidenciaM3(sp.GetService<IPrevidenciaM3Service>());

        await processDataM3.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM3Type.PK56);
        await processDataM3.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM3Type.PK57);
        await processDataM3.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM3Type.PK58);


        var processDataM4 = new ProcessDataPrevidenciaM4(sp.GetService<IPrevidenciaM4Service>());

        await processDataM4.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM4Type.PK05);
        await processDataM4.ProcessFilesAsync(inputPath, outputPath, processedFilesPath, CVP.Routines.MotorArquivosComunicacao.Enums.PrevidenciaM4Type.PK06);
        
        Console.WriteLine("Arquivos processados com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao processar arquivos: {ex.Message}");
    }
};

timer.Start();

System.Console.WriteLine("Rotina iniciada. Pressione Enter para encerrar.");
Console.ReadLine();

timer.Stop();
timer.Dispose();

async Task ExecutarRotina(Func<Task> blocoExecucao)
{
    bool ocorreuErro = false;
    ILogPersist logPersist = sp.GetService<ILogPersist>();
    IExecucaoRotinaPersist execucaoRotinaPersist = sp.GetService<IExecucaoRotinaPersist>();

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
