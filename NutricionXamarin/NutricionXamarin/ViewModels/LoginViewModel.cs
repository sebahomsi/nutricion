using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace NutricionXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private string _usuario;
        private string _password;
        private bool _recordame;
        private bool _isRunning;
        private bool _isEnable;

        public string Usuario
        {
            get => _usuario;
            set => SetValue(ref _usuario, value);
        }

        public string Password
        {
            get => _password;
            set => SetValue(ref _password, value);
        }

        public bool Recordame
        {
            get => _recordame;
            set => SetValue(ref _recordame, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetValue(ref _isRunning, value);
        }

        public bool IsEnable
        {
            get => _isEnable;
            set => SetValue(ref _isEnable, value);
        }

        public ICommand IngresarCommand => new Command(Login);

        private void Login()
        {
            throw new NotImplementedException();
        }
    }
}
