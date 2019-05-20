using Dominio.Entidades;
using Infraestructura.Contexto;
using System;
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
            //AddEstablecimientos(context);
            //AddGrupos(context);
            //AddSubGrupos(context);
            //AddUnidades(context);
            //AddAlimentos(context);
            //AddPacientes(context);
            //AddRecetas(context);
            //AddOpcionesDetalles(context);
            //AddPatologias(context);
            //AddAlergiasIntolerancias(context);
            //AddMicroNutrientes(context);
        }

        private void AddEstablecimientos(NutricionDbContext context)
        {
            context.Establecimientos.AddOrUpdate(x => x.Id, 
                new Establecimiento() { Id = 1, Direccion = "",Email = "",Telefono = "",Horario = "",Facebook="",Nombre ="Solana Novillo",Instagram = "" });
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

        private static void AddMicroNutrientes(NutricionDbContext context)
        {
            context.MicroNutrientes.AddOrUpdate(x => x.Id,
                new MicroNutriente() { Id = 1, Codigo = 1, Descripcion = "Calcio", Eliminado = false },
                new MicroNutriente() { Id = 2, Codigo = 2, Descripcion = "Hierro", Eliminado = false },
                new MicroNutriente() { Id = 3, Codigo = 3, Descripcion = "Vitamina C", Eliminado = false },
                new MicroNutriente() { Id = 4, Codigo = 4, Descripcion = "Magnesio", Eliminado = false },
                new MicroNutriente() { Id = 5, Codigo = 5, Descripcion = "Potasio", Eliminado = false });
        }

        private static void AddPatologias(NutricionDbContext context)
        {
            context.Patologias.AddOrUpdate(x => x.Id,
                new Patologia() { Id = 1, Codigo = 1, Descripcion = "Dolor de garganta", Eliminado = false },
                new Patologia() { Id = 2, Codigo = 2, Descripcion = "Dolor de oido", Eliminado = false },
                new Patologia() { Id = 3, Codigo = 3, Descripcion = "IVU", Eliminado = false },
                new Patologia() { Id = 4, Codigo = 4, Descripcion = "Bronquitis", Eliminado = false },
                new Patologia() { Id = 5, Codigo = 5, Descripcion = "Bronquiolitis", Eliminado = false });
        }

        private static void AddAlergiasIntolerancias(NutricionDbContext context)
        {
            context.AlergiasIntolerancias.AddOrUpdate(x => x.Id,
                new AlergiaIntolerancia() { Id = 1, Codigo = 1, Descripcion = "Alergia al polen", Eliminado = false },
                new AlergiaIntolerancia() { Id = 2, Codigo = 2, Descripcion = "Alergia al pelo de gato", Eliminado = false },
                new AlergiaIntolerancia() { Id = 3, Codigo = 3, Descripcion = "Alergia al látex", Eliminado = false },
                new AlergiaIntolerancia() { Id = 4, Codigo = 4, Descripcion = "Intolerante a la lactosa", Eliminado = false },
                new AlergiaIntolerancia() { Id = 5, Codigo = 5, Descripcion = "Intolerante al gluten", Eliminado = false });
        }

        private static void AddAlimentos(NutricionDbContext context)
        {
            context.Alimentos.AddOrUpdate(x => x.Id,
                new Alimento() { Id = 1, Codigo = 1, SubGrupoId = 11, Descripcion = "Fideos", Eliminado = false, MacroNutriente = new MacroNutriente()
                {
                    Id = 1,
                    Calorias = 109,
                    Energia = 5,
                    Grasa = 9,
                    HidratosCarbono = 4,
                    Proteina = 3,
                    Eliminado = false
                }},
                new Alimento() { Id = 2, Codigo = 2, SubGrupoId = 15, Descripcion = "Melón", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 2,
                        Calorias = 113,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 5,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 3, Codigo = 3, SubGrupoId = 3, Descripcion = "Queso", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 3,
                        Calorias = 100,
                        Energia = 5,
                        Grasa = 8,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 4, Codigo = 4, SubGrupoId = 14, Descripcion = "Bife de Hígado", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 4,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 5, Codigo = 5, SubGrupoId = 14, Descripcion = "Carne Molida", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 5,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 6, Codigo = 6, SubGrupoId = 13, Descripcion = "Pechuga de Pollo", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 6,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 7, Codigo = 7, SubGrupoId = 17, Descripcion = "Lechuga", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 7,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 8, Codigo = 8, SubGrupoId = 17, Descripcion = "Acelga", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 8,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 9, Codigo = 9, SubGrupoId = 17, Descripcion = "Repollo", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 9,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento() { Id = 10, Codigo = 10, SubGrupoId = 18, Descripcion = "Sal", Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 10,
                        Calorias = 109,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                },
                new Alimento()
                {
                    Id = 11,
                    Codigo = 11,
                    SubGrupoId = 18,
                    Descripcion = "Salsa",
                    Eliminado = false,
                    MacroNutriente = new MacroNutriente()
                    {
                        Id = 11,
                        Calorias = 209,
                        Energia = 5,
                        Grasa = 9,
                        HidratosCarbono = 4,
                        Proteina = 3,
                        Eliminado = false
                    }
                });
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

        private static void AddRecetas(NutricionDbContext context)
        {
            context.Opciones.AddOrUpdate(x=> x.Id,
                new Opcion()
                {
                    Id = 1,
                    Codigo = 1,
                    Eliminado = false,
                    Descripcion = "Hamburguejas al vapor"
                },
                new Opcion()
                {
                    Id = 2,
                    Codigo = 2,
                    Eliminado = false,
                    Descripcion = "Fideos con salsa y queso"
                },
                new Opcion()
                {
                    Id = 3,
                    Codigo = 3,
                    Eliminado = false,
                    Descripcion = "Ensalada de frutas"
                },
                new Opcion()
                {
                    Id = 4,
                    Codigo = 4,
                    Eliminado = false,
                    Descripcion = "Bife de higado con ensalada"
                });
        }

        private static void AddOpcionesDetalles(NutricionDbContext context)
        {
            context.OpcionesDetalles.AddOrUpdate(x => x.Id,
                new OpcionDetalle()
                {
                    Id = 1,
                    Codigo = 1,
                    OpcionId = 1,
                    AlimentoId = 5,
                    Cantidad = 200,
                    UnidadMedidaId = 2,
                    Eliminado = false
                },
                new OpcionDetalle()
                {
                    Id = 2,
                    Codigo = 2,
                    OpcionId = 1,
                    AlimentoId = 10,
                    Cantidad = 10,
                    UnidadMedidaId = 2,
                    Eliminado = false
                },
                new OpcionDetalle()
                {
                    Id = 3,
                    Codigo = 3,
                    OpcionId = 1,
                    AlimentoId = 3,
                    Cantidad = 50,
                    UnidadMedidaId = 2,
                    Eliminado = false
                },
                new OpcionDetalle()
                {
                    Id = 4,
                    Codigo = 4,
                    OpcionId = 2,
                    AlimentoId = 1,
                    Cantidad = 100,
                    UnidadMedidaId = 2,
                    Eliminado = false
                },
                new OpcionDetalle()
                {
                    Id = 5,
                    Codigo = 5,
                    OpcionId = 2,
                    AlimentoId = 11,
                    Cantidad = 4,
                    UnidadMedidaId = 3,
                    Eliminado = false
                },
                new OpcionDetalle()
                {
                    Id = 6,
                    Codigo = 6,
                    OpcionId = 2,
                    AlimentoId = 3,
                    Cantidad = 80,
                    UnidadMedidaId = 2,
                    Eliminado = false
                });
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
                    EstablecimientoId = 1,
                    Eliminado = false
                });

            context.Personas.AddOrUpdate(x => x.Id,
                new Paciente()
                {
                    Id = 2,
                    Codigo = 2,
                    Nombre = "Pepe",
                    Apellido = "Argento",
                    Celular = "+5479154402",
                    Dni = "30251898",
                    Direccion = "Italia 2350",
                    FechaNac = new DateTime(1980, 10, 20),
                    Foto = "~/Content/Imagenes/user-icon.jpg",
                    Mail = "pepeargento@gmail.com",
                    Sexo = 1,
                    Telefono = "4784899",
                    TieneObservacion = false,
                    EstablecimientoId = 1,
                    Eliminado = false
                });

            context.Personas.AddOrUpdate(x => x.Id,
                new Paciente()
                {
                    Id = 3,
                    Codigo = 3,
                    Nombre = "Oliver",
                    Apellido = "Atom",
                    Celular = "+5479090124",
                    Dni = "41257898",
                    Direccion = "Balon 262",
                    FechaNac = new DateTime(1989, 08, 17),
                    Foto = "~/Content/Imagenes/user-icon.jpg",
                    Mail = "oliveratom@gmail.com",
                    Sexo = 1,
                    Telefono = "4123099",
                    TieneObservacion = false,
                    EstablecimientoId = 1,
                    Eliminado = false
                });
        }

    }
}
