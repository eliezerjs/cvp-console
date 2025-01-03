using CVP.Routines.MotorArquivosComunicacao.Enums;
using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces.Capas;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Services.Capas
{
    public class CapaFacSimplesColoridoService : ICapaFacSimplesColoridoService
    {
        public Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  CapaFacColoridoType tipo)
        {
            throw new NotImplementedException();
        }

        public byte[] GerarCapaFacSimplesColorido(Dictionary<string, string> dadosBoleto, CapaFacColoridoType tipo)
        {
            throw new NotImplementedException();
        }
    }
}
