using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Usuario
{
    public interface IUsuarioServicio
    {
        void Crear(string nombreUsuario, string nombreRol,long establecimientoId);

        Task Crear(string nombreUsuario, string passwword, string nombreRol, long establecimientoId);

        Task<bool> Actualizar(string nombreUsuario, string nombreUsuarioNuevo);

        Task<bool> ActualizarPassword(string nombreUsuario, string password);

        string CrearNombreUsuario(string apellido, string nombre);
    }
}
