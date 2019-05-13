using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Servicio.Interface.Usuario;
using Servicio.Usuario.Dto;
using System.Threading.Tasks;
using static Aplicacion.Conexion.ConexionDb;


namespace Servicio.Usuario
{
    public class UsuarioServicio : ServicioBase, IUsuarioServicio
    {
        private readonly IdentityDbContext<ApplicationUser> IdentityDbContext;

        public UsuarioServicio()
        {
            IdentityDbContext = new IdentityDbContext<ApplicationUser>(ObtenerCadenaConexion);
        }


        public async Task<bool> Actualizar(string nombreUsuario, string nombreUsuarioNuevo)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(IdentityDbContext));

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
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(IdentityDbContext));

            var usuario = await userManager.FindByEmailAsync(nombreUsuario);

            if (usuario == null) return false;

            usuario.PasswordHash = password;

            var resultado = await userManager.UpdateAsync(usuario);

            IdentityDbContext.Dispose();

            return resultado.Succeeded;
        }

        public void Crear(string nombreUsuario, string nombreRol, long establecimientoId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(IdentityDbContext));

            var user = userManager.FindByName(nombreUsuario);

            if (user == null)
            {

                var usuarioNuevo = new ApplicationUser
                {
                    Email = $"{nombreUsuario.Replace("ñ", "n")}",
                    UserName = nombreUsuario.Replace("ñ", "n"),
                    EstablecimientoId = establecimientoId
                };

                userManager.Create(usuarioNuevo, "P$assword");
                userManager.AddToRoles(usuarioNuevo.Id, nombreRol);
            }
        }

        public async Task Crear(string nombreUsuario, string passwword, string nombreRol, long establecimientoId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(IdentityDbContext));

            var user = userManager.FindByName(nombreUsuario);

            if (user == null)
            {
                var usuarioNuevo = new ApplicationUser
                {
                    Email = nombreUsuario,
                    UserName = nombreUsuario,
                    EstablecimientoId = establecimientoId
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

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(IdentityDbContext));

            while (userManager.FindByName(nombreUsuario) != null)
            {
                contador++;
                nombreUsuario = $"{nombre.Trim().Substring(0, contador)}{apellido.Trim()}";
            }

            return nombreUsuario.Replace(" ", "").Replace("ñ", "n");
        }
    }
}
