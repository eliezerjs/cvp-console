using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IBoletoM2Service
    {
        byte[] GerarBoletoM2(Dictionary<string, string> dadosBoleto, BoletoM2Type tipo);

        Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM2Type tipo);
    }
}
