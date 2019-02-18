using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCore.Repositorio;

namespace NutricionMovil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class home : ContentPage
	{
		public home ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var repositorio = new Repositorio();

            var lista = repositorio.ObtenerTodos<Paciente>();

            ListaPaciente.ItemsSource = lista;
            ListaPaciente.ItemSelected += ListaContact_ItemSelected;
        }

        private async void ListaContact_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var paciente = e.SelectedItem as Paciente;

                var repositorio = new Repositorio();

                var pacienteDetalles = repositorio.ObtenerPorId<Paciente>(paciente.Id);

                await Navigation.PushModalAsync(new PacienteDetalle(pacienteDetalles));

                //var planAlimenticio = new StringBuilder();

                //planAlimenticio.Append(pacienteDetalles.ApyNom + "\n");

                //foreach (var planAli in pacienteDetalles.PlanesAlimenticios)
                //{
                //    planAlimenticio.Append("Plan Alimenticio" + "\n");
                //    planAlimenticio.Append("Codigo:" + planAli.Codigo +"\n");
                //    planAlimenticio.Append("Motivo:" + planAli.Comentarios + "\n");
                //    planAlimenticio.Append("Fecha:" + planAli.Fecha + "\n");
                //}

                //await DisplayAlert("Listas",planAlimenticio.ToString() , "Aceptar");
            }
        }
    }
}