using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CVP.Routines.MotorArquivosComunicacao.Console.Interfaces
{
    public interface IImportFileConverterService
    {        
        string ConvertToJson(Stream fileStream);

        string ConvertFileToJson(Stream fileStream);
    }
}
