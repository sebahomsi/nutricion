using System.Web.Mvc;
using Servicio.AlergiaIntolerancia;
using Servicio.Alimento;
using Servicio.Comida;
using Servicio.DatoAnalitico;
using Servicio.DatoAntropometrico;
using Servicio.Dia;
using Servicio.Empleado;
using Servicio.Grupo;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.Alimento;
using Servicio.Interface.Comida;
using Servicio.Interface.DatoAnalitico;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Dia;
using Servicio.Interface.Empleado;
using Servicio.Interface.Grupo;
using Servicio.Interface.MacroNutriente;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.MicroNutrienteDetalle;
using Servicio.Interface.Observacion;
using Servicio.Interface.Paciente;
using Servicio.Interface.Patologia;
using Servicio.Interface.Persona;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.SubGrupo;
using Servicio.MacroNutriente;
using Servicio.MicroNutriente;
using Servicio.MicroNutrienteDetalle;
using Servicio.Observacion;
using Servicio.Paciente;
using Servicio.Patologia;
using Servicio.Persona;
using Servicio.PlanAlimenticio;
using Servicio.SubGrupo;
using Unity;
using Unity.Mvc5;

namespace Aplicacion.IoC
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {

            container.RegisterType<IAlergiaIntoleranciaServicio, AlergiaIntoleranciaServicio>();
            container.RegisterType<IAlimentoServicio, AlimentoServicio>();
            container.RegisterType<IComidaServicio, ComidaServicio>();
            container.RegisterType<IDatoAnaliticoServicio, DatoAnaliticoServicio>();
            container.RegisterType<IDiaServicio, DiaServicio>();
            container.RegisterType<IDatoAntropometricoServicio, DatoAntropometricoServicio>();
            container.RegisterType<IEmpleadoServicio, EmpleadoServicio>();
            container.RegisterType<IGrupoServicio, GrupoServicio>();
            container.RegisterType<IMacroNutrienteServicio, MacroNutrienteServicio>();
            container.RegisterType<IMicroNutrienteServicio, MicroNutrienteServicio>();
            container.RegisterType<IMicroNutrienteDetalleServicio, MicroNutrienteDetalleServicio>();
            container.RegisterType<IObservacionServicio, ObservacionServicio>();
            container.RegisterType<IPacienteServicio, PacienteServicio>();
            container.RegisterType<IPatologiaServicio, PatologiaServicio>();
            container.RegisterType<ISexoServicio, SexoServicio>();
            container.RegisterType<IPlanAlimenticioServicio, PlanAlimenticioServicio>();
            container.RegisterType<ISubGrupoServicio, SubGrupoServicio>();


            //esto ya venia
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}