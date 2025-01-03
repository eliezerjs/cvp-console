using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM5Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK12()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PORTABILIDADE_DT_CEDENTE", 410, 243, 8, false),

                ("PORTABILIDADE_ENTIDADE_CEDENTE", 107, 257, 8, false),
                ("PORTABILIDADE_NO_CERTIFICADO", 427, 257, 8, false),

                ("PORTABILIDADE_VALOR_LIQUIDO", 113, 272, 8, false),
                ("PORTABILIDADE_ENTIDADE_CESSIONARIA", 296, 272, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK13()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PORTABILIDADE_DT_CEDENTE", 423, 226, 8, false),

                ("PORTABILIDADE_ENTIDADE_CEDENTE", 200, 241, 8, false),
                ("PORTABILIDADE_VALOR_LIQUIDO", 457, 241, 8, false),

                ("PORTABILIDADE_VALOR_LIQUIDO", 110, 256, 8, false),
                ("PORTABILIDADE_NO_CERTIFICADO", 450, 255, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK14()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PORTABILIDADE_DT_CEDENTE", 385, 217, 8, false),

                ("PORTABILIDADE_ENTIDADE_CEDENTE", 107, 230, 7, false),
                ("PORTABILIDADE_NO_CERTIFICADO", 282, 230, 8, false),
                ("PORTABILIDADE_VALOR_LIQUIDO", 465, 230, 8, false),

            };
        }

        public string GetImagePath(PrevidenciaM5Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
