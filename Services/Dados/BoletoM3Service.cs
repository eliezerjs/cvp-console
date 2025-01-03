using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM3Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposSeguro()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                //inicio linha 1
                ("AGENCIA", 48, 182,9, false),
                ("APOLICE", 138, 182,9, false),
                ("FATURA", 215, 182,9, false),
                ("PERIODO", 282, 182,9, false),
                ("EMISSAO", 410, 182,9, false),
                ("VENCIMENT", 486, 182,9, false),
                //fim

                //inicio linha 2
                ("ESTIPULANTE", 48, 215,9, false),
                ("CNPJ1", 346, 215,9, false),

                //inicio linha 3
                ("ENDERE1", 48, 249,9, false),

                //inicio linha 4
                ("CEP1", 47, 281,9, false),
                ("CIDADE1", 159, 281,9, false),
                ("EST1", 499, 281,9, false),

                //inicio linha 5
                ("SUBESTIPULANTE", 47, 314, 8, false),
                ("CNPJ2", 406, 314,9, false),

                //inicio linha 6
                ("ENDERE2", 47, 350,9, false),
                ("CEP2", 49, 382,9, false),
                ("CIDADE2", 159, 382,9, false),
                ("EST2", 487, 382,9, false),

                //inicio linha 7
                ("NVIDAS", 49, 415,9, false),
                ("CAPITAL", 160, 415,9, false),
                ("IOF", 312, 415,9, false),
                ("PREMIO", 412, 415,9, false)
            };
        }

        public string GetImagePath(BoletoM3Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
