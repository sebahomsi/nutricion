using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Servicio.Interface.Alimento;

namespace Bridge
{
    public class LacteoServicio: ILacteosService
    {
        public IList<AlimentoDto> ListarLacteos()
        {
            string filePath = @"C:\Users\Ezequiel\Desktop\Lacteos.xls";

            var retorno= new AlimentoDto();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet();



                }
            }
            return new List<AlimentoDto>();
        }
    }
}
