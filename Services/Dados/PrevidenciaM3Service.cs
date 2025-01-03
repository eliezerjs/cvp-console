using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM3Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK56()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                 // Dados do participante/segurado
                ("COBERTURA_PROTECAO_NM_PART_PROTECAO", 67, 123, 8, false),
                ("COBERTURA_PROTECAO_CPF_PROTECAO", 380, 123, 8, false),
                ("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 496, 123, 8, false),

                // Dados do plano
                ("COBERTURA_PROTECAO_NM_TABUA_BIOMETRICA", 67, 177, 8, false),
                ("", 300, 165, 8, false),
                ("", 500, 170, 8, false),

                ("COBERTURA_PROTECAO_NM_COBERTURA", 67, 214, 8, false),
                ("", 320, 214, 8, false),
                ("COBERTURA_PROTECAO_NO_CERTIFICADO", 500, 213, 8, false),

                ("PRODUTO_DT_INIC_VIG", 67, 247, 8, false),
                ("PRODUTO_Plano_uni_men", 300, 247, 8, false),
                ("PRODUTO_Debito_boleto", 500, 247, 8, false),

                // Cobertura de sobrevivência
                ("PRODUTO_NM_BENEFICIO_BASICO", 67, 308, 8, false),
                ("COBERTURA_PROTECAO_CD_PROC_SUSEP", 450, 308, 8, false),
                
                ("PRODUTO_DT_CONC_BENEF", 67, 343, 8, false),
                ("PRODUTO_TP_REGIME_TRIB", 300, 343, 8, false),
                ("PRODUTO_VL_CONTRIB_PARTIC", 500, 343, 8, false),

                ("COBERTURA_PROTECAO_NM_PART_PROTECAO", 67, 397, 8, false),
                ("COBERTURA_PROTECAO_CPF_PROTECAO", 380, 397, 8, false),
                ("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 496, 397, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK57()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                  // Dados do participante/segurado
                ("COBERTURA_PROTECAO_NM_PART_PROTECAO", 67, 127, 8, false),
                ("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 496, 127, 8, false),

                ("COBERTURA_PROTECAO_NM_PART_PROTECAO", 67, 397, 8, false),
                ("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 260, 397, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK58()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                  // Dados do participante/segurado
                ("COBERTURA_PROTECAO_NM_PART_PROTECAO", 67, 130, 8, false),
                ("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 496, 130, 8, false),

                ("COBERTURA_PROTECAO_NM_PART_PROTECAO", 67, 387, 8, false),
                ("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 250, 390, 8, false)
            };
        }

        public string GetImagePath(PrevidenciaM3Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
