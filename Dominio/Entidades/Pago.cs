﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("Pagos")]
    [MetadataType(typeof(IPago))]
    public class Pago : EntidadBase
    {
        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public string Concepto { get; set; }

        public double Monto { get; set; }

        public bool EstaEliminado { get; set; }

        public long PacienteId { get; set; }

        //propiedad de navegacion

        public virtual Paciente Paciente { get; set; }


    }

}
