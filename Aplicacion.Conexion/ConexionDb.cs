namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"LUCAS\LUCAS";
        private const string BaseDatos = "Nutricion";
        private const string Usuario = "sa";
        private const string Password = "minimi";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
