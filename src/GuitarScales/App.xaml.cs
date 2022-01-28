using System.Windows;

namespace GuitarScales;

public partial class App : Application
{
    public App()
    {
        DependencyRegistration.RegisterDependencies();
    }
}