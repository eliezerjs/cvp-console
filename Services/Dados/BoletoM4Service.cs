using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM4Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVA18()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 59, 65, 8, false),
                ("COD_PROD", 308, 65, 8, false),
                ("PROC_SUSEP", 394, 65, 8, false),

                ("NOME_CLIENTE", 55, 109, 8, false),
                ("CPF", 327, 109, 8, false),
                ("DT_NASC", 456, 109, 8, false),

                ("NUMDOCTO", 495, 402, 8, false),

                ("CEDENTE", 405, 423, 8, false),
                ("DTVENCTO", 507, 423, 8, false),

                ("NSNUMERO", 377, 443, 8, false),
                ("VALDOCTO", 524, 443, 8, false),

                ("NUMOBJETO", 520, 479, 8, false),

                ("PARCELA", 470, 525, 8, false),
                ("DTVENCTO", 508, 525, 8, false),

                ("CEDENTE", 495, 545, 8, false),

                ("DTDOCTO", 86, 562, 8, false),
                ("NUMDOCTO", 158, 562, 8, false),
                ("DTPROCESS", 396, 562, 8, false),
                ("NSNUMERO", 466, 562, 8, false),

                ("VALDOCTO", 524, 579, 8, false),

                ("VALDOCTO", 524, 659, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVA24()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 84, 8, false),
                ("COD_PROD", 308, 84, 8, false),
                ("PROC_SUSEP", 394, 84, 8, false),

                ("NOME_CLIENTE", 56, 130, 8, false),
                ("CPF", 330, 130, 8, false),
                ("DT_NASC", 457, 130, 8, false),

                //("NUMDOCTO", 495, 364, 8, false),

                ("CEDENTE", 380, 384, 8, false),
                ("DTVENCTO", 505, 384, 8, false),

                ("NSNUMERO", 373, 404, 8, false),
                ("VALDOCTO", 525, 404, 8, false),

                //("NUMOBJETO", 515, 525, 8, false),

                ("PARCELA", 470, 491, 8, false),
                ("DTVENCTO", 505, 491, 8, false),

                ("CEDENTE", 492, 509, 8, false),

                ("DTDOCTO", 83, 526, 8, false),
                //("NUMDOCTO", 155, 526, 8, false),
                ("DTPROCESS", 390, 526, 8, false),
                //("NSNUMERO", 525, 526, 8, false),

                ("VALDOCTO", 525, 543, 8, false),

                ("VALDOCTO", 525, 623, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA23()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 67, 8, false),
                ("COD_PROD", 307, 67, 8, false),
                ("PROC_SUSEP", 393, 67, 8, false),

                ("NOME_CLIENTE", 55, 109, 8, false),
                ("CPF", 327, 109, 8, false),
                ("DT_NASC", 453, 109, 8, false),

                ("NUMDOCTO", 495, 390, 8, false),

                ("CEDENTE", 405, 410, 8, false),
                ("DTVENCTO", 507, 410, 8, false),

                ("NSNUMERO", 377, 430, 8, false),
                ("VALDOCTO", 526, 430, 8, false),

                ("NUMOBJETO", 517, 466, 8, false),

                ("PARCELA", 470, 514, 8, false),
                ("DTVENCTO", 512, 514, 8, false),

                ("CEDENTE", 493, 531, 8, false),

                ("DTDOCTO", 86, 549, 8, false),
                ("NUMDOCTO", 155, 549, 8, false),
                ("DTPROCESS", 390, 549, 8, false),
                ("NSNUMERO", 475, 549, 7, false),

                ("VALDOCTO", 526, 566, 8, false),

                ("VALDOCTO", 526, 646, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA24()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 81, 8, false),
                ("COD_PROD", 308, 81, 8, false),
                ("PROC_SUSEP", 394, 81, 8, false),

                ("NOME_CLIENTE", 56, 127, 8, false),
                ("CPF", 327, 127, 8, false),
                ("DT_NASC", 454, 128, 8, false),

                ("NUMDOCTO", 495, 459, 8, false),

                ("CEDENTE", 405, 479, 8, false),
                ("DTVENCTO", 507, 479, 8, false),

                ("NSNUMERO", 377, 499, 8, false),
                ("VALDOCTO", 522, 499, 8, false),

                ("NUMOBJETO", 517, 535, 8, false),

                ("PARCELA", 470, 586, 8, false),
                ("DTVENCTO", 507, 586, 8, false),

                ("CEDENTE", 493, 604, 8, false),

                ("DTDOCTO", 86, 621, 8, false),
                ("NUMDOCTO", 157, 621, 8, false),
                ("DTPROCESS", 390, 621, 8, false),
                ("NSNUMERO", 464, 621, 8, false),

                ("VALDOCTO", 522, 638, 8, false),

                ("VALDOCTO", 521, 718, 8, false)
            };
        }
        public string GetImagePath(BoletoM4Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }

    }
}
