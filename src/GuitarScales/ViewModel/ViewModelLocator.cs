namespace GuitarScales.ViewModel;

public class ViewModelLocator
{
    public IMainViewModel MainViewModel => IoCContainer.Resolve<IMainViewModel>();
}