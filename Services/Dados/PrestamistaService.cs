using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrestamistaService
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposPrest01()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 59, 250, 8, false),
                ("NOME_PRODUTO", 59, 288, 8, false),
                ("NUM_CERTIF", 59, 320, 8, false),
                ("DT_VIG", 59, 356, 8, false),
                ("CUSTO", 60, 470, 8, false),
                ("OPCAO_PAG", 60, 510, 8, false)
            };
        }

        public string GetImagePath(PrestamistaType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
