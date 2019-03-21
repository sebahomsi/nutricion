using Servicio.AlergiaIntolerancia;
using Servicio.Alimento;
using Servicio.Comida;
using Servicio.DatoAnalitico;
using Servicio.DatoAntropometrico;
using Servicio.Dia;
using Servicio.Empleado;
using Servicio.Establecimiento;
using Servicio.Grupo;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.Alimento;
using Servicio.Interface.Comida;
using Servicio.Interface.DatoAnalitico;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Dia;
using Servicio.Interface.Empleado;
using Servicio.Interface.Establecimiento;
using Servicio.Interface.Grupo;
using Servicio.Interface.MacroNutriente;
using Servicio.Interface.Mensaje;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.MicroNutrienteDetalle;
using Servicio.Interface.Observacion;
using Servicio.Interface.ObservacionAlergiaIntolerancia;
using Servicio.Interface.ObservacionAlimento;
using Servicio.Interface.ObservacionPatologia;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;
using Servicio.Interface.Paciente;
using Servicio.Interface.Pago;
using Servicio.Interface.Patologia;
using Servicio.Interface.Persona;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.Receta;
using Servicio.Interface.RecetaDetalle;
using Servicio.Interface.Rol;
using Servicio.Interface.SubGrupo;
using Servicio.Interface.Turno;
using Servicio.Interface.UnidadMedida;
using Servicio.Interface.Usuario;
using Servicio.MacroNutriente;
using Servicio.Mensaje;
using Servicio.MicroNutriente;
using Servicio.MicroNutrienteDetalle;
using Servicio.Observacion;
using Servicio.ObservacionAlergiaIntolerancia;
using Servicio.ObservacionAlimento;
using Servicio.ObservacionPatologia;
using Servicio.Opcion;
using Servicio.OpcionDetalle;
using Servicio.Paciente;
using Servicio.Pago;
using Servicio.Patologia;
using Servicio.Persona;
using Servicio.PlanAlimenticio;
using Servicio.Receta;
using Servicio.RecetaDetalle;
using Servicio.Rol;
using Servicio.SubGrupo;
using Servicio.Turno;
using Servicio.UnidadMedida;
using Servicio.Usuario;
using System.Web.Mvc;
using Servicio.Estado;
using Servicio.Interface.Estado;
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
            container.RegisterType<IUnidadMedidaServicio, UnidadMedidaServicio>();
            container.RegisterType<IOpcionServicio, OpcionServicio>();
            container.RegisterType<IOpcionDetalleServicio, OpcionDetalleServicio>();
            container.RegisterType<ITurnoServicio, TurnoServicio>();
            container.RegisterType<IObservacionAlergiaIntoleranciaServicio, ObservacionAlergiaIntoleranciaServicio>();
            container.RegisterType<IObservacionAlimentoServicio, ObservacionAlimentoServicio>();
            container.RegisterType<IObservacionPatologiaServicio, ObservacionPatologiaServicio>();
            container.RegisterType<IEstablecimientoServicio, EstablecimientoServicio>();
            container.RegisterType<IMensajeServicio, MensajeServicio>();
            container.RegisterType<IRecetaServicio, RecetaServicio>();
            container.RegisterType<IRecetaDetalleServicio, RecetaDetalleServicio>();
            container.RegisterType<IPagoServicio, PagoServicio>();
            container.RegisterType<IEstadoServicio, EstadoServicio>();

            container.RegisterType<IUsuarioServicio, UsuarioServicio>();
            container.RegisterType<IRolServicio, RolServicio>();


            //esto ya venia
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}