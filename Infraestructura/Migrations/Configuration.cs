using System;
using Dominio.Entidades;
using Infraestructura.Contexto;
using System.Data.Entity.Migrations;
using System.Linq;

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
            AddUnidades(context);
            AddAlimentos(context);
            AddPacientes(context);
        }

        private static void AddSubGrupos(NutricionDbContext context)
        {
            context.SubGrupos.AddOrUpdate(x => x.Id,
                new SubGrupo() { Id = 1, Codigo = 1, Descripcion = "Derivados", GrupoId = 1, Eliminado = false},
                new SubGrupo() { Id = 2, Codigo = 2, Descripcion = "Leche-Bebidas Lacteas", GrupoId = 1, Eliminado = false },
                new SubGrupo() { Id = 3, Codigo = 3, Descripcion = "Quesos", GrupoId = 1, Eliminado = false},
                new SubGrupo() { Id = 4, Codigo = 4, Descripcion = "Al natural", GrupoId = 2, Eliminado = false},
                new SubGrupo() { Id = 5, Codigo = 5, Descripcion = "Otras bebidas", GrupoId = 2, Eliminado = false},
                new SubGrupo() { Id = 6, Codigo = 6, Descripcion = "Procesados", GrupoId = 2, Eliminado = false},
                new SubGrupo() { Id = 7, Codigo = 7, Descripcion = "Panes", GrupoId = 3, Eliminado = false},
                new SubGrupo() { Id = 8, Codigo = 8, Descripcion = "Arroz", GrupoId = 3, Eliminado = false },
                new SubGrupo() { Id = 9, Codigo = 9, Descripcion = "Galletas", GrupoId = 3, Eliminado = false },
                new SubGrupo() { Id = 10, Codigo = 10, Descripcion = "Maiz", GrupoId = 3, Eliminado = false },
                new SubGrupo() { Id = 11, Codigo = 11, Descripcion = "Pastas", GrupoId = 3, Eliminado = false },
                new SubGrupo() { Id = 12, Codigo = 12, Descripcion = "Al natural", GrupoId = 3, Eliminado = false },
                new SubGrupo() { Id = 13, Codigo = 13, Descripcion = "Carnes Blancas", GrupoId = 4, Eliminado = false },
                new SubGrupo() { Id = 14, Codigo = 14, Descripcion = "Carnes Rojas", GrupoId = 4, Eliminado = false },
                new SubGrupo() { Id = 15, Codigo = 15, Descripcion = "Tropicales", GrupoId = 5, Eliminado = false },
                new SubGrupo() { Id = 16, Codigo = 16, Descripcion = "Citricas", GrupoId = 5, Eliminado = false },
                new SubGrupo() { Id = 17, Codigo = 17, Descripcion = "Hojas y tallos tiernos", GrupoId = 6, Eliminado = false },
                new SubGrupo() { Id = 18, Codigo = 18, Descripcion = "Condimentos", GrupoId = 7, Eliminado = false });
        }

        private static void AddGrupos(NutricionDbContext context)
        {
            context.Grupos.AddOrUpdate(x => x.Id,
                new Grupo() {Id = 1, Codigo = 1, Descripcion = "Lacteos", Eliminado = false},
                new Grupo() {Id = 2, Codigo = 2, Descripcion = "Frutos secos y semillas", Eliminado = false},
                new Grupo() {Id = 3, Codigo = 3, Descripcion = "Cereales", Eliminado = false},
                new Grupo() {Id = 4, Codigo = 4, Descripcion = "Carnes", Eliminado = false},
                new Grupo() { Id = 5, Codigo = 5, Descripcion = "Frutas", Eliminado = false },
                new Grupo() { Id = 6, Codigo = 6, Descripcion = "Verduras", Eliminado = false },
                new Grupo() { Id = 7, Codigo = 7, Descripcion = "Salsas, Condimentos y Especias", Eliminado = false });
        }

        private static void AddAlimentos(NutricionDbContext context)
        {
            context.Alimentos.AddOrUpdate(x => x.Id,
                new Alimento() { Id = 1, Codigo = 1, SubGrupoId = 11, Descripcion = "Fideos", Eliminado = false },
                new Alimento() { Id = 2, Codigo = 2, SubGrupoId = 15, Descripcion = "Melón", Eliminado = false },
                new Alimento() { Id = 3, Codigo = 3, SubGrupoId = 3, Descripcion = "Queso", Eliminado = false },
                new Alimento() { Id = 4, Codigo = 4, SubGrupoId = 14, Descripcion = "Bife de Hígado", Eliminado = false },
                new Alimento() { Id = 5, Codigo = 5, SubGrupoId = 14, Descripcion = "Carne Molida", Eliminado = false },
                new Alimento() { Id = 6, Codigo = 6, SubGrupoId = 13, Descripcion = "Pechuga de Pollo", Eliminado = false },
                new Alimento() { Id = 7, Codigo = 7, SubGrupoId = 17, Descripcion = "Lechuga", Eliminado = false },
                new Alimento() { Id = 8, Codigo = 8, SubGrupoId = 17, Descripcion = "Acelga", Eliminado = false },
                new Alimento() { Id = 9, Codigo = 9, SubGrupoId = 17, Descripcion = "Repollo", Eliminado = false },
                new Alimento() { Id = 10, Codigo = 10, SubGrupoId = 18, Descripcion = "Sal", Eliminado = false });
        }

        private static void AddUnidades(NutricionDbContext context)
        {
            context.UnidadMedidas.AddOrUpdate(x => x.Id,
                new UnidadMedida()
                {
                    Id = 1,
                    Codigo = 1,
                    Descripcion = "Kilogramo",
                    Abreviatura = "kg",
                    Eliminado = false
                },
                new UnidadMedida() {Id = 2, Codigo = 2, Descripcion = "Gramo", Abreviatura = "g", Eliminado = false},
                new UnidadMedida()
                {
                    Id = 3,
                    Codigo = 3,
                    Descripcion = "Cucharada",
                    Abreviatura = "cda",
                    Eliminado = false
                },
                new UnidadMedida()
                {
                    Id = 4,
                    Codigo = 4,
                    Descripcion = "Cucharadita",
                    Abreviatura = "ctda",
                    Eliminado = false
                },
                new UnidadMedida() {Id = 5, Codigo = 5, Descripcion = "Unidad", Abreviatura = "u", Eliminado = false});

        }

        private static void AddPacientes(NutricionDbContext context)
        {
            context.Personas.AddOrUpdate(x => x.Id,
                new Paciente()
                {
                    Id = 1,
                    Codigo = 1,
                    Nombre = "Juanito",
                    Apellido = "Jones",
                    Celular = "+5479090902",
                    Dni = "34657898",
                    Direccion = "Camino del Sirga 267",
                    FechaNac = new DateTime(1990,10,20),
                    Foto = "~/Content/Imagenes/user-icon.jpg",
                    Mail = "juanitojones@gmail.com",
                    Sexo = 1,
                    Telefono = "4390099",
                    TieneObservacion = false,
                    Eliminado = false
                });
        }

    }
}
