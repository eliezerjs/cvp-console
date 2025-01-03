using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IBoletoM4Service
    {
        byte[] GerarBoletoM4(Dictionary<string, string> dadosBoleto, BoletoM4Type tipo);
        Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM4Type tipo);
    }
}
