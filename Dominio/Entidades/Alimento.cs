using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("Alimentos")]
    [MetadataType(typeof(IAlimento))]

    public class Alimento : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long SubGrupoId { get; set; }

        public bool Eliminado { get; set; }

        public long? MacroNutrienteId { get; set; }

        public bool TieneMacroNutriente { get; set; }

        //Navigation Properties
        public virtual ICollection<MicroNutrienteDetalle> MicroNutrienteDetalles { get; set; }
        public virtual ICollection<Observacion> Observaciones { get; set; }
        public virtual SubGrupo SubGrupo { get; set; }
        //public virtual MacroNutriente MacroNutriente { get; set; }
        public virtual ICollection<Opcion> Opciones { get; set; }
    }
}
