﻿namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"SEBAHOMSI\SQLEXPRESS";
        private const string BaseDatos = "Nutricion2Db";
        private const string Usuario = "sa";
        private const string Password = "dumas123";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}