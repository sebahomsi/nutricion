using System;

namespace NutricionXamarin.ViewModels
{
    public class MainViewModel
    {
        //atributos
        private static MainViewModel _instancia;

        //propiedades
        public LoginViewModel Login { get; set; }

        public MainViewModel()
        {
            _instancia = this;
            Login = new LoginViewModel();
            //LoadMenu();
        }

        private void LoadMenu()
        {
            throw new NotImplementedException();
        }

        public static MainViewModel ObtenerInstancia()
        {
            return _instancia ?? new MainViewModel();
        }

    }
}
