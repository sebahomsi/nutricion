﻿namespace Aplicacion.Conexion
{
    public static class ConexionDb
    {
        private const string Servidor = @"DESKTOP-MH85TLT\SEBA";
        private const string BaseDatos = "Nutricion00a1";
        private const string Usuario = "sa";
        private const string Password = "dumas123";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; Initial Catalog={BaseDatos}; User Id={Usuario}; Password={Password}";
    }
}
