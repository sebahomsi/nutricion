﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Estrategia : EntidadBase
    {
        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}
