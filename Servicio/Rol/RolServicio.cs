using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Servicio.Interface.Rol;
using static Aplicacion.Conexion.ConexionDb;



namespace Servicio.Rol
{
    public class RolServicio : IRolServicio
    {
        private readonly IdentityDbContext IdentityContext;

        public RolServicio()
        {
            IdentityContext = new IdentityDbContext(ObtenerCadenaConexion);
        }

        public async Task Crear(string nombreRol)
        {
            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(IdentityContext));

            if (!await rolManager.RoleExistsAsync(nombreRol))
            {
                await rolManager.CreateAsync(new IdentityRole(nombreRol));
            }
        }
    }
}
