using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("Recetas")]
    [MetadataType(typeof(IReceta))]
    public class Receta : EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }

        public virtual ICollection<Alimento> Alimentos { get; set; }
    }
}
