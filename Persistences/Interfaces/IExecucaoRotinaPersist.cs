namespace CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces
{
    public interface IExecucaoRotinaPersist
    {
        long GravarExecucao();
        bool AtualizarExecucao(long codigoExecucaoRotina, bool ocorreuErro);
    }
}