namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"nutricionnovillo2.mssql.somee.com";
        private const string BaseDatos = "nutricionnovillo2";
        private const string Usuario = "novillosolana";
        private const string Password = "solana456";
        //private const string Servidor = @"DESKTOP-TTT7KKD";
        //private const string BaseDatos = "NutriProduction";
        //private const string Usuario = "sa";
        //private const string Password = "pa$$word";


        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
