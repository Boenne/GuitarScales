using GuitarScales.Messenger;
using GuitarScales.ViewModel;

namespace GuitarScales;

public class DependencyRegistration
{
    public static void RegisterDependencies()
    {
        IoCContainer.Register<IMainViewModel, MainViewModel>();
        IoCContainer.RegisterSingleton<IMessengerWrapper, MessengerWrapper>();
        IoCContainer.RegisterSingleton<INoteInitializer, NoteInitializer>();
    }
}