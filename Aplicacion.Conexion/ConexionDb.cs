﻿namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"nutricionnovillo2.mssql.somee.com";
        private const string BaseDatos = "nutricionnovillo2";
        private const string Usuario = "novillosolana";
        private const string Password = "solana456";
        //private const string Servidor = @"DESKTOP-MH85TLT\SEBAHOMSI";
        //private const string BaseDatos = "NutriDoctrollProduction";
        //private const string Usuario = "sa";
        //private const string Password = "dumas123";


        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
