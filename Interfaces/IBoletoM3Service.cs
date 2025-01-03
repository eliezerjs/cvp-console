using CVP.Routines.MotorArquivosComunicacao.Enums;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IBoletoM3Service
    {
        byte[] GerarBoletoM3(Dictionary<string, string> dadosBoleto, BoletoM3Type tipo);

        Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM3Type tipo);

    }
}
