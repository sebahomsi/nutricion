namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"DESKTOP-TTT7KKD";
        private const string BaseDatos = "Nutricion0031";
        private const string Usuario = "sa";
        private const string Password = "pa$$word";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
