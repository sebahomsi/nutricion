namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"LUCAS-PC";
        private const string BaseDatos = "Nutricion0025";
        private const string Usuario = "sa";
        private const string Password = "minimi";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
