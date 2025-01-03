using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaM1Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK28()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 477, 8, false),
                ("PRODUTO_NM_PRODUTO", 70, 490, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 490, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 70, 505, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK29()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 513, 8, false),
                ("PRODUTO_NM_PRODUTO", 70, 525, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 525, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 70, 540, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK30()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 535, 8, false),
                ("PRODUTO_NM_PRODUTO", 77, 547, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 547, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 77, 563, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK31()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 529, 8, false),
                ("PRODUTO_NM_PRODUTO", 75, 540, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 330, 540, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 75, 555, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK32()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 450, 8, false),
                ("PRODUTO_NM_PRODUTO", 75, 462, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 462, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 75, 477, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK33()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 487, 8, false),
                ("PRODUTO_NM_PRODUTO", 75, 499, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 499, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 75, 514, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK34()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 527, 8, false),
                ("PRODUTO_NM_PRODUTO", 75, 540, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 540, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 75, 555, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK36()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",190, 496, 8, false),
                ("PRODUTO_NM_PRODUTO", 77, 508, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 508, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 77, 525, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK47()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_DT_CONC_BENEF",100, 532, 8, false),
                ("PRODUTO_NM_PRODUTO", 77, 544, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 320, 544, 8, false),
                ("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 77, 559, 8, false),
            };
        }

        public string GetImagePath(PrevidenciaM1Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
