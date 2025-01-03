using CVP.Routines.MotorArquivosComunicacao.Console.Persistences.Interfaces;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Persistences
{
    public class PesquisaPersist : IPesquisaPersist
    {
        public string DbFake()
        {
            return "DB";
        }
    }
}
