using CVP.Routines.MotorArquivosComunicacao.Enums;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class PrevidenciaOutrosService
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK11()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_TITULO_FORMULARIO", 97, 456, 8, false),
                ("PRODUTO_NO_PROPOSTA", 367, 456, 8, false),

                ("PRODUTO_NO_CERTIFICADO", 367, 515, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK15()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",180, 201, 8, false),

                ("BOLETO_DS_TEXTO", 70, 300, 8, false),
                ("BOLETO_NO_CERTIFICADO", 280, 300, 8, false),
                ("BOLETO_VENCIMENTO", 425, 300, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK35()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 174, 8, false),

                ("BOLETO_NM_CEDENTE", 70, 227, 8, false),
                ("BOLETO_NO_CPF_CNPJ", 445, 227, 8, false),

                ("BOLETO_DS_TEXTO", 70, 270, 8, false),
                ("BOLETO_NO_CERTIFICADO", 447, 270, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK37()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_TITULO_FORMULARIO", 105, 571, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 327, 571, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK44()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_TITULO_FORMULARIO", 133, 324, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 388, 324, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK48()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_TITULO_FORMULARIO", 123, 281, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 350, 281, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK49()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_TITULO_FORMULARIO", 373, 397, 8, false),
                ("PRODUTO_NO_CERTIFICADO", 125, 410, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK53()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO_NM_TITULO_FORMULARIO", 260, 469, 8, false),
                ("PRODUTO_NO_PROPOSTA", 450, 469, 8, false)
            };
        }
        public string GetImagePath(PrevidenciaOutrosType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
