using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class BoletoM1Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVD02()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("AGENCIA", 45, 133, 8, false),
                ("APOLICE", 125, 133, 8, false),
                ("FATURA", 197, 133, 8, false),
                ("PERIODO", 260, 133, 8, false),
                ("EMISSAO", 407, 133, 8, false),
                ("VENCIMENT", 481, 133, 8, false),
                ("ESTIPULANTE", 45, 157, 8, false),
                ("ENDERECO", 260, 157, 8, false),
                ("CEP", 45, 182, 8, false),
                ("CIDADE", 154, 182, 8, false),
                ("UF", 407, 182, 8, false),
                ("CNPJ", 453, 182, 8, false),
                ("ESTIPULANTE", 45, 205, 8, false),
                ("ENDERECO", 260, 205, 8, false),
                ("CEP", 45, 230, 8, false),
                ("CIDADE", 154, 230, 8, false),
                ("UF", 407, 230, 8, false),
                ("CNPJ", 453, 230, 8, false),
                ("NVIDAS", 45, 252, 8, false),
                ("CAPITAL", 154, 252, 8, false),
                ("IOF", 310, 252, 8, false),
                ("PREMIO", 407, 252, 8, false),

                ("NUMDOCTO", 493, 378, 8, false),

                ("AGENCIA", 442, 398, 8, false),
                ("VENCIMENT", 507, 398, 8, false),

                ("NSNUMERO", 377, 418, 8, false),
                ("VALDOCTO", 512, 418, 8, false),

                ("PARCELA", 476, 534, 8, false),

                ("NUMCDBARRA", 198, 355, 12, true),
                ("NUMCDBARRA", 198, 515, 12, true),

                ("VENCIMENT", 507, 534, 8, false),

                ("AGENCIA", 530, 552, 8, false),

                ("DTDOCTO", 85, 568, 8, false),
                ("CODDOC", 157, 568, 8, false),
                ("AGENCIA", 273, 568, 8, false),
                ("AGENCIA", 415, 568, 8, false),
                ("NSNUMERO", 464, 568, 8, false),

                ("VALDOCTO", 512, 585, 8, false),
            
                ("VALDOCTO", 512, 666, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA25()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("AGENCIA", 45, 133, 8, false),
                ("APOLICE", 125, 133, 8, false),
                ("FATURA", 197, 133, 8, false),
                ("PERIODO", 260, 133, 8, false),
                ("EMISSAO", 407, 133, 8, false),
                ("VENCIMENT", 481, 133, 8, false),

                ("ESTIPULANTE", 45, 157, 8, false),
                ("ENDERECO", 260, 157, 8, false),

                ("CEP", 45, 181, 8, false),
                ("CIDADE", 154, 181, 8, false),
                ("UF", 407, 181, 8, false),
                ("CNPJ", 453, 181, 8, false),

                ("ESTIPULANTE", 45, 205, 8, false),
                ("ENDERECO", 260, 205, 8, false),

                ("CEP", 45, 228, 8, false),
                ("CIDADE", 154, 228, 8, false),
                ("UF", 407, 228, 8, false),
                ("CNPJ", 453, 228, 8, false),

                ("NVIDAS", 45, 252, 8, false),
                ("CAPITAL", 154, 252, 8, false),
                ("IOF", 310, 252, 8, false),
                ("PREMIO", 522, 252, 8, false),

                ("NUMDOCTO", 493, 378, 8, false),

                ("AGENCIA", 442, 398, 8, false),
                ("VENCIMENT", 507, 398, 8, false),

                ("NSNUMERO", 377, 418, 8, false),
                ("VALDOCTO", 522, 418, 8, false),

                ("PARCELA", 476, 534, 8, false),

                ("NUMCDBARRA", 198, 355, 12, true),
                ("NUMCDBARRA", 198, 515, 12, true),

                ("VENCIMENT", 507, 534, 8, false),

                ("AGENCIA", 530, 552, 8, false),

                ("DTDOCTO", 85, 568, 8, false),
                ("CODDOC", 157, 568, 8, false),
                ("AGENCIA", 273, 568, 8, false),
                ("AGENCIA", 415, 568, 8, false),
                ("NSNUMERO", 464, 568, 8, false),

                ("VALDOCTO", 522, 585, 8, false),

                ("VALDOCTO", 522, 666, 8, false),

                ("SUBESTIPULANTE", 50, 687, 8, false),

                ("ENDERE2", 50, 695, 8, false),

                ("CIDADE2", 50, 703, 8, false),
                ("EST2", 100, 703, 8, false),

                ("CEP2" +
                "", 50, 711, 8, false),
            };
        }

        public string GetImagePath(BoletoM1Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
