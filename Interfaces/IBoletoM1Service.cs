using CVP.Routines.MotorArquivosComunicacao.Enums;
using iText.Forms.Form.Element;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IBoletoM1Service
    {
        byte[] GerarBoletoM1(Dictionary<string, string> dadosBoleto, BoletoM1Type tipo);

        Task<byte[]> ConverterEGerarPdfAsync(Stream fileStream,  BoletoM1Type tipo);
    }
}
