using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Rol
{
    public interface IRolServicio
    {
        Task Crear(string nombreRol);
    }
}
