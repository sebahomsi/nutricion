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
		}

	}
}