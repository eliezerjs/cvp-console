using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM2Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK08()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_PRODUTO",382, 485, 8, false),
                ("PRODUTO_NO_PROPOSTA", 70, 560, 8, false),
                ("PARTICIPANTE_NM_CLIENTE", 230, 560, 8, false),
                ("PRODUTO_VL_CONTRIB_PARTIC", 405, 560, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK09()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_PRODUTO",380, 450, 8, false),
                ("PRODUTO_NO_PROPOSTA", 70, 527, 8, false),
                ("PARTICIPANTE_NM_CLIENTE", 235, 527, 8, false),
                ("PRODUTO_VL_CONTRIB_PARTIC", 420, 527, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK10()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_PRODUTO",387, 448, 8, false),
                ("PRODUTO_NO_PROPOSTA", 70, 540, 8, false),
                ("PARTICIPANTE_NM_CLIENTE", 215, 540, 8, false),
                ("PRODUTO_VL_CONTRIB_EMPR", 435, 540, 8, false),
                ("PRODUTO_VL_CONTRIB_PARTIC", 495, 540, 8, false)
            };
        }

        public string GetImagePath(PrevidenciaM2Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
