using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NutricionXamarin.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T bakingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(bakingField, value))
            {
                return;
            }
            bakingField = value;
            OnPropertyChanged(propertyName);
        }

    }
}
