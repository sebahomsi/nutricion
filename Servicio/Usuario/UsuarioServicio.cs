using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Servicio.Interface.Usuario;
using static Aplicacion.Conexion.ConexionDb;


namespace Servicio.Usuario
{
    public class UsuarioServicio : ServicioBase, IUsuarioServicio
    {
        private readonly IdentityDbContext IdentityDbContext;

        public UsuarioServicio()
        {
            IdentityDbContext = new IdentityDbContext(ObtenerCadenaConexion);
        }


        public async Task<bool> Actualizar(string nombreUsuario, string nombreUsuarioNuevo)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(IdentityDbContext));

            var usuario = await userManager.FindByEmailAsync(nombreUsuario);

            if (usuario == null) return false;

            usuario.Email = nombreUsuarioNuevo;
            usuario.UserName = nombreUsuarioNuevo;

            var resultado = await userManager.UpdateAsync(usuario);

            IdentityDbContext.Dispose();

            return resultado.Succeeded;
        }

        public async Task<bool> ActualizarPassword(string nombreUsuario, string password)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(IdentityDbContext));

            var usuario = await userManager.FindByEmailAsync(nombreUsuario);

            if (usuario == null) return false;

            usuario.PasswordHash = password;

            var resultado = await userManager.UpdateAsync(usuario);

            IdentityDbContext.Dispose();

            return resultado.Succeeded;
        }

        public void Crear(string nombreUsuario, string nombreRol)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(IdentityDbContext));

            var user = userManager.FindByName(nombreUsuario);

            if (user == null)
            {

                var usuarioNuevo = new IdentityUser
                {
                    Email = $"{nombreUsuario.Replace("ñ", "n")}",
                    UserName = nombreUsuario.Replace("ñ", "n")
                };

                userManager.Create(usuarioNuevo, "P$assword");
                userManager.AddToRoles(usuarioNuevo.Id, nombreRol);
            }
        }

        public async Task Crear(string nombreUsuario, string passwword, string nombreRol)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(IdentityDbContext));

            var user = userManager.FindByName(nombreUsuario);

            if (user == null)
            {
                var usuarioNuevo = new IdentityUser
                {
                    Email = nombreUsuario,
                    UserName = nombreUsuario
                };

                await userManager.CreateAsync(usuarioNuevo, passwword);
                await userManager.AddToRolesAsync(usuarioNuevo.Id, nombreRol);
            }

            IdentityDbContext.Dispose();
        }

        public string CrearNombreUsuario(string apellido, string nombre)
        {
            var contador = 1;
            var nombreUsuario = $"{nombre.Trim().Substring(0, contador)}{apellido.Trim()}";

            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(IdentityDbContext));

            while (userManager.FindByName(nombreUsuario) != null)
            {
                contador++;
                nombreUsuario = $"{nombre.Trim().Substring(0, contador)}{apellido.Trim()}";
            }

            return nombreUsuario.Replace(" ", "").Replace("ñ", "n");
        }
    }
}
