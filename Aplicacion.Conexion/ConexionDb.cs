namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"ALEJANDRO\SQLEXPRESS";
        private const string BaseDatos = "Nutricion000011";
        private const string Usuario = "sa";
        private const string Password = "hola123";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
