﻿namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        //private const string Servidor = @"NutricionNovillo2.mssql.somee.com";
        //private const string BaseDatos = "NutricionNovillo2";
        //private const string Usuario = "novilloSolana";
        //private const string Password = "solana456";

        private const string Servidor = @"LUCAS\LUCAS";
        private const string BaseDatos = "NutriProd";
        private const string Usuario = "sa";
        private const string Password = "minimi";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
