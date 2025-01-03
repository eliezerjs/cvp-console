using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM2Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVD32()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("AGENCIA", 35, 107, 8, false),
                ("APOLICE", 120, 107, 8, false),
                ("FATURA", 210, 107, 8, false),
                ("PERIODO", 300, 107, 7, false),
                ("EMISSAO", 390, 107, 8, false),
                ("VENCIMENT", 481, 107, 8, false),

                ("ESTIPULANTE", 35, 133, 8, false),
                ("ENDERECO", 300, 133, 8, false),

                ("CEP", 35, 160, 8, false),
                ("CIDADE", 120, 160, 8, false),
                ("UF", 390, 160, 8, false),
                ("CNPJ1", 481, 160, 8, false),

                ("ESTIPULANTE", 35, 187, 8, false),
                ("ENDERECO", 300, 187, 8, false),

                ("CEP", 35, 214, 8, false),
                ("CIDADE", 120, 214, 8, false),
                ("UF", 390, 214, 8, false),
                ("CNPJ1", 481, 214, 8, false),

                ("NVIDAS", 35, 241, 8, false),
                ("CAPITAL", 120, 241, 8, false),
                ("IOF", 300, 241, 8, false),
                ("PREMIO", 538, 241, 8, false),

                ("NUMCDBARRA", 197, 300, 10, false),

                ("NUMDOCTO", 507, 329, 8, false),

                ("AGENCIA", 450, 353, 8, false),
                ("VENCIMENT", 521, 353, 8, false),

                ("NSNUMERO", 386, 377, 8, false),
                ("VALDOCTO", 537, 377, 8, false),

                ("NUMCDBARRA", 197, 450, 10, false),

                ("PARCELA", 487, 482, 8, false),
                ("VENCIMENT", 522, 482, 8, false),

                ("AGENCIA", 545, 503, 8, false),

                ("DTDOCTO", 50, 525, 8, false),
                ("CODDOC", 127, 525, 8, false),
                ("AGENCIA", 230, 525, 8, false),
                ("AGENCIA", 395, 525, 8, false),
                ("NSNUMERO", 478, 525, 8, false),

                ("AGENCIA", 315, 547, 8, false),
                ("VALOR", 395, 547, 8, false),
                ("VALDOCTO", 538, 547, 8, false),

                ("", 540, 568, 8, false),

                ("", 540, 590, 8, false),

                ("", 540, 611, 8, false),

                ("", 540, 631, 8, false),

                ("VALDOCTO", 538, 653, 8, false),

                ("SUBESTIPULANTE", 50, 672, 8, false),

                ("ENDERE2", 50, 680, 8, false),

                ("CIDADE2", 50, 687, 8, false),
                ("EST2", 100, 687, 8, false),

                ("CEP2", 50, 695, 8, false),
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA26()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("AGENCIA", 35, 107, 8, false),
                ("APOLICE", 120, 107, 8, false),
                ("FATURA", 210, 107, 8, false),
                ("PERIODO", 300, 107, 7, false),
                ("EMISSAO", 390, 107, 8, false),
                ("VENCIMENT", 481, 107, 8, false),

                ("ESTIPULANTE", 35, 133, 8, false),
                ("ENDERECO", 300, 133, 8, false),

                ("CEP", 35, 160, 8, false),
                ("CIDADE", 120, 160, 8, false),
                ("UF", 390, 160, 8, false),
                ("CNPJ1", 481, 160, 8, false),

                ("ESTIPULANTE", 35, 187, 8, false),
                ("ENDERECO", 300, 187, 8, false),

                ("CEP", 35, 214, 8, false),
                ("CIDADE", 120, 214, 8, false),
                ("UF", 390, 214, 8, false),
                ("CNPJ1", 481, 214, 8, false),

                ("NVIDAS", 35, 241, 8, false),
                ("CAPITAL", 120, 241, 8, false),
                ("IOF", 300, 241, 8, false),
                ("PREMIO", 538, 241, 8, false),

                ("NUMCDBARRA", 197, 300, 10, false),

                ("NUMDOCTO", 507, 329, 8, false),

                ("AGENCIA", 450, 353, 8, false),
                ("VENCIMENT", 521, 353, 8, false),

                ("NSNUMERO", 386, 377, 8, false),
                ("VALDOCTO", 537, 377, 8, false),

                ("NUMCDBARRA", 197, 450, 10, false),

                ("PARCELA", 487, 482, 8, false),
                ("VENCIMENT", 522, 482, 8, false),

                ("AGENCIA", 545, 503, 8, false),

                ("DTDOCTO", 50, 525, 8, false),
                ("CODDOC", 127, 525, 8, false),
                ("AGENCIA", 230, 525, 8, false),
                ("AGENCIA", 395, 525, 8, false),
                ("NSNUMERO", 478, 525, 8, false),

                ("AGENCIA", 315, 547, 8, false),
                ("VALOR", 395, 547, 8, false),
                ("VALDOCTO", 538, 547, 8, false),

                ("", 540, 568, 8, false),

                ("", 540, 590, 8, false),

                ("", 540, 611, 8, false),

                ("", 540, 631, 8, false),

                ("VALDOCTO", 538, 653, 8, false),

                ("SUBESTIPULANTE", 50, 672, 8, false),

                ("ENDERE2", 50, 680, 8, false),

                ("CIDADE2", 50, 687, 8, false),
                ("EST2", 100, 687, 8, false),

                ("CEP2" +
                "", 50, 695, 8, false),
            };
        }

        public string GetImagePath(BoletoM2Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
