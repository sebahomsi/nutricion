using NutricionXamarin.ViewModels;

namespace NutricionXamarin.Infraestructura
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
