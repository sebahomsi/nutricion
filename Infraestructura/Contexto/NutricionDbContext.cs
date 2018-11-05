using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aplicacion.Conexion.ConexionDb;

namespace Infraestructura.Contexto
{
    public class NutricionDbContext: DbContext
    {
        public NutricionDbContext() : base(ObtenerCadenaConexion)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            // Activa Migraciones Automaticas
            Database.SetInitializer(
            new MigrateDatabaseToLatestVersion<NutricionDbContext,
            Migrations.Configuration>());
        }

        public IDbSet<Persona> Personas { get; set; }
        public IDbSet<Alimento> Alimentos { get; set; }
        public IDbSet<Grupo> Grupos { get; set; }
        public IDbSet<SubGrupo> SubGrupos { get; set; }
        public IDbSet<MacroNutriente> MacroNutrientes { get; set; }
        public IDbSet<MicroNutriente> MicroNutrientes { get; set; }
        public IDbSet<DatoAnalitico> DatosAnaliticos { get; set; }
        public IDbSet<DatoAntropometrico> DatosAntropometricos { get; set; }
        public IDbSet<MicroNutrienteDetalle> MicroNutrienteDetalles { get; set; }
        public IDbSet<AlergiaIntolerancia> AlergiasIntolerancias { get; set; }
        public IDbSet<Patologia> Patologias { get; set; }
        public IDbSet<Dia> Dias { get; set; }
        public IDbSet<Comida> Comidas { get; set; }
        public IDbSet<Opcion> Opciones { get; set; }
        public IDbSet<OpcionDetalle> OpcionesDetalles { get; set; }
        public IDbSet<Observacion> Observaciones { get; set; }
        public IDbSet<PlanAlimenticio> PlanesAlimenticios { get; set; }
        public IDbSet<UnidadMedida> UnidadMedidas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MacroNutriente>().HasRequired(x => x.Alimento).WithOptional(t => t.MacroNutriente);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
