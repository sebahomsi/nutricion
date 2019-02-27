using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutricionMovil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteDetalle : ContentPage
	{
        public Paciente _paciente { get; set; }

		public PacienteDetalle (Paciente paciente)
		{
			InitializeComponent ();
            _paciente = paciente;


            ApyNom.Text = _paciente.ApyNom;
            Dni.Text = _paciente.Dni;
            Direccion.Text = _paciente.Direccion;
            Mail.Text = _paciente.Mail;
            Edad.Text = _paciente.Edad.ToString();
            SexoStr.Text = _paciente.SexoStr;
            Telefono.Text = _paciente.Telefono;

            var datoAntropometrico = _paciente.DatosAntropometricos.Last();
            if(datoAntropometrico != null)
            {
                Peso.Text = datoAntropometrico.Peso;
                Altura.Text = datoAntropometrico.Altura;
                MasaCorporal.Text = datoAntropometrico.MasaCorporal;
                MasaGrasa.Text = datoAntropometrico.MasaGrasa;
                PerimetroCadera.Text = datoAntropometrico.PerimetroCadera;
                PerimetroCintura.Text = datoAntropometrico.PerimetroCintura;
            }

            var datoAnalitico = _paciente.DatosAnaliticos.Last();
            if (datoAnalitico != null)
            {
                ColesterolHdl.Text = datoAnalitico.ColesterolHdl;
                ColesterolLdl.Text = datoAnalitico.ColesterolLdl;
                ColesterolTotal.Text = datoAnalitico.ColesterolTotal;
                PresionDiastolica.Text = datoAnalitico.PresionDiastolica;
                PresionSistolica.Text = datoAnalitico.PresionSistolica;
                Trigliceridos.Text = datoAnalitico.Trigliceridos;
            }

            var turno = _paciente.Turnos.Last();
            if(turno != null)
            {
                DiaTurno.Text = turno.FechaEntradaStr;
                HoraTurno.Text = turno.HorarioEntradaStr;
                UltimoMotivo.Text = turno.Motivo;
            }

            var plan = _paciente.PlanesAlimenticios.Last();
            if(plan != null)
            {
                MotivoPlan.Text = plan.Motivo;
                FechaImplementacion.Text = plan.FechaStr;
                ComentarioPlan.Text = plan.Comentarios;
            }
        }

	}
}