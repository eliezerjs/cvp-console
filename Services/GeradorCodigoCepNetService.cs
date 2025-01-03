using CVP.Routines.MotorArquivosComunicacao.Console.Interfaces;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services;

public class GeradorCodigoCepNetService : IGeradorCodigoCepNetService
{
    public string GerarCodigo(string COD_CEP, string clienteCif, string categoriaRegiao, string cnae)
    {
        string d1 = COD_CEP.Substring(0, 1);
        string d2 = COD_CEP.Substring(1, 1);
        string d3 = COD_CEP.Substring(2, 1);
        string d4 = COD_CEP.Substring(3, 1);
        string d5 = COD_CEP.Substring(4, 1);
        string d6 = COD_CEP.Substring(5, 1);
        string d7 = COD_CEP.Substring(6, 1);
        string d8 = COD_CEP.Substring(7, 1);

        int digitoControle = int.Parse(d1) + int.Parse(d2) + int.Parse(d3) + int.Parse(d4) +
                             int.Parse(d5) + int.Parse(d6) + int.Parse(d7) + int.Parse(d8);

        int multi10 = (digitoControle / 10 + 1) * 10;
        int digContole = multi10 - digitoControle;

        return COD_CEP.Substring(0, 5) + COD_CEP.Substring(5, 3) + "00000" +
               COD_CEP.Substring(0, 5) + COD_CEP.Substring(5, 3) + "00001" +
               digContole.ToString("D2") + "01" + clienteCif + "0000000000" +
               categoriaRegiao + "000000000000000" + cnae + "|";
    }

    private static bool CepSintaxe(string cep)
    {
        string tabela = "0123456789";
        cep = cep.Replace(" ", "").Replace(".", "").Replace("-", "");

        if (cep.Length != 8 || cep == "00000000")
        {
            return false;
        }

        foreach (char c in cep)
        {
            if (!tabela.Contains(c.ToString()))
            {
                return false;
            }
        }

        return true;
    }
}
