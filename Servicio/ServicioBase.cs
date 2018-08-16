using Infraestructura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class ServicioBase
    {
        protected NutricionDbContext Context;

        public ServicioBase()
        {
            Context = new NutricionDbContext();
        }
    }
}
