using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM6Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK17()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO", 380, 218, 8, false),

                ("BOLETO_DT_VENCTO", 160, 270, 8, false),
                ("BOLETO_VL_DOCTO", 293, 270, 8, false),
                ("BOLETO_VL_DOCTO", 385, 270, 8, false),

                ("BOLETO_DT_VENCTO", 235, 329, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK21()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {

                ("BOLETO_DT_VENCTO", 185, 270, 8, false),
                ("BOLETO_VL_DOCTO", 307, 270, 8, false),
                ("BOLETO_VL_DOCTO", 397, 270, 8, false),

                ("BOLETO_DT_VENCTO", 165, 333, 8, false)
            };
        }

        public string GetImagePath(PrevidenciaM6Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
