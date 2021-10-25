using System.Threading.Tasks;

namespace DevNullCore.Bus.Interfaces
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler { }

}
