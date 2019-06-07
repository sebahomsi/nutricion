﻿using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Opciones")]
    [MetadataType(typeof(IOpcion))]

    public class Opcion : EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public long? ComentarioId { get; set; }


        //Propiedades de Navegacion
        public virtual Comentario Comentario { get; set; }
        public virtual ICollection<ComidaDetalle> ComidasDetalles { get; set; }
        public virtual ICollection<OpcionDetalle> OpcionDetalles { get; set; }
        public virtual ICollection<SubGrupoReceta> SubGruposRecetas { get; set; }
    }
}
