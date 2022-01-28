using LightInject;

namespace GuitarScales;

public class IoCContainer
{
    public static ServiceContainer ServiceContainer = new();

    public static void Register<TInterface, TImplementation>() where TImplementation : TInterface
    {
        ServiceContainer.Register<TInterface, TImplementation>();
    }

    public static void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
    {
        ServiceContainer.Register<TInterface, TImplementation>(new PerContainerLifetime());
    }

    public static TInterface Resolve<TInterface>()
    {
        return ServiceContainer.GetInstance<TInterface>();
    }
}