using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoasVindasService
    {
        public List<(string Key, float X, float Y, float FontSize, bool IsBold)> GetCamposVIDA05()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("", 67, 55, 8, false),

                ("SEGURADO", 55, 71, 8, false),

                ("SEGURADO", 59, 225, 8, false),

                ("NOME_PRODUTO", 59, 253, 8, false),

                ("NUM_CERTIF", 59, 288, 8, false),

                ("DT_VIG", 59, 320, 8, false),

                ("CUSTO", 60, 465, 8, false),

                ("OPCAO_PAG", 60, 475, 8, false),

                ("COD_PRODUTO", 380, 748, 7, false),

                ("COD_SUSEP", 465, 748, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA07()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 125, 208, 11, false),
                ("NUM_CERTIF", 210, 370, 16, false),
                ("COD_PRODUTO", 376, 787, 6, false), 
                ("COD_SUSEP", 428, 787, 6, false),
            };
        }

        public string GetImagePath(BoasVindasType tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
