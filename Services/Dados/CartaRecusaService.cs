using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class CartaRecusaService
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA01()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 141, 9, false),
                ("NUM_PROPOSTA", 383, 166, 8, false),
                ("DATA_DECLINIO", 115, 177, 8, false),
                ("COD_PRODUTO", 311, 728, 7, false),
                ("COD_SUSEP", 55, 738, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA02()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 141, 9, false),
                ("NUM_PROPOSTA", 383, 166, 8, false),
                ("DATA_DECLINIO", 115, 177, 8, false),
                ("COD_PRODUTO", 345, 728, 7, false),
                ("COD_SUSEP", 55, 738, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA03()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 141, 9, false),
                ("NUM_PROPOSTA", 383, 166, 8, false),
                ("DATA_DECLINIO", 115, 177, 8, false),
                ("COD_PRODUTO", 298, 740, 7, false),
                ("COD_SUSEP", 373, 740, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA04()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 141, 9, false),
                ("NUM_PROPOSTA", 383, 166, 8, false),
                ("DATA_DECLINIO", 115, 176, 8, false),
                ("COD_PRODUTO", 297, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public string GetImagePath(CartaRecusaType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
