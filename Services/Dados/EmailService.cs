using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class EmailService
    {
        public List<(string Key, float X, float Y, float FontSize, bool IsBold)> GetCamposVD09()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool IsBold)>
            {
                ("SEGURADO", 80, 199, 10, false),
                ("COD_PRODUTO", 356, 748, 7, false),
                ("COD_SUSEP", 442, 748, 7, false),
                ("COD_SUSEPCAP", 314, 767, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA18()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 80, 199, 10, false),
                ("COD_PRODUTO", 359, 748, 6, false),
                ("COD_SUSEP", 444, 748, 6, false),
                ("COD_SUSEPCAP", 350, 768, 6, false)
            };
        }
        //Campo Exclusivo
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA17()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 100, 174, 11, false),
                ("NUM_CERTIF", 195, 324, 13, false),
                ("COD_PRODUTO", 376, 787, 6, false), //white
                ("COD_SUSEP", 428, 787, 6, false),
                //("COD_SUSEPCAP", 100, 799, 5, "White")
            };
        }

        //Campo Exclusivo
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVD08()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 100, 174, 11, false),
                ("NUM_CERTIF", 195, 324, 13, false),
                ("COD_PRODUTO", 376, 787, 6, false), //white
                ("COD_SUSEP", 428, 787, 6, false), //white
                //("COD_SUSEPCAP", 100, 799, 5, "White")
            };
        }



        public string GetImagePath(EmailType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
