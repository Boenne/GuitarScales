using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace GuitarScales.Messenger;

public interface IMessengerWrapper
{
    void Register<TRecipient, TMessage>(TRecipient recipient, MessageHandler<TRecipient, TMessage> action)
        where TRecipient : ObservableRecipient where TMessage : class;

    void Send<TMessage>(TMessage message) where TMessage : class;
}

public class MessengerWrapper : ObservableRecipient, IMessengerWrapper
{
    public void Register<TRecipient, TMessage>(TRecipient recipient, MessageHandler<TRecipient, TMessage> action)
        where TRecipient : ObservableRecipient where TMessage : class
    {
        Messenger.Register(recipient, action);
    }

    public void Send<TMessage>(TMessage message) where TMessage : class
    {
        Messenger.Send(message);
    }
}