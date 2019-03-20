using Dominio.Entidades;
using Infraestructura.Contexto;
using System.Data.Entity.Migrations;
namespace Infraestructura.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<NutricionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NutricionDbContext context)
        {
            AddGrupos(context);
            AddSubGrupos(context);
        }

        private static void AddSubGrupos(NutricionDbContext context)
        {
            context.SubGrupos.AddOrUpdate(x => x.Id,
                new SubGrupo() {Id = 1, Codigo = 1, Descripcion = "Derivados", GrupoId = 1, Eliminado = false},
                new SubGrupo() {Id = 2, Codigo = 2, Descripcion = "Leche-Bebidas Lacteas", GrupoId = 1, Eliminado = false },
                new SubGrupo() {Id = 3, Codigo = 3, Descripcion = "Queso", GrupoId = 1, Eliminado = false},
                new SubGrupo() {Id = 4, Codigo = 4, Descripcion = "Al natural", GrupoId = 2, Eliminado = false},
                new SubGrupo() {Id = 5, Codigo = 5, Descripcion = "Otras bebidas", GrupoId = 2, Eliminado = false},
                new SubGrupo() {Id = 6, Codigo = 6, Descripcion = "Procesados", GrupoId = 2, Eliminado = false},
                new SubGrupo() {Id = 7, Codigo = 7, Descripcion = "Pan", GrupoId = 3, Eliminado = false});
        }

        private static void AddGrupos(NutricionDbContext context)
        {
            context.Grupos.AddOrUpdate(x => x.Id,
                new Grupo() {Id = 1, Codigo = 1, Descripcion = "Lacteos", Eliminado = false},
                new Grupo() {Id = 2, Codigo = 2, Descripcion = "Frutos secos y semillas", Eliminado = false},
                new Grupo() {Id = 3, Codigo = 3, Descripcion = "Cereales", Eliminado = false},
                new Grupo() {Id = 4, Codigo = 4, Descripcion = "Carnes", Eliminado = false},
                new Grupo() {Id = 5, Codigo = 5, Descripcion = "Frutas", Eliminado = false});
        }

    }
}
