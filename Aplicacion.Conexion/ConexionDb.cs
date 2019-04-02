namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"DESKTOP-0H5QIP9\MSSQLSERVERR";
        private const string BaseDatos = "Nutricion0028";
        private const string Usuario = "sa";
        private const string Password = "hola123";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
