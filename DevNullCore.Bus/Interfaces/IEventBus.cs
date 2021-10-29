namespace DevNullCore.Bus.Interfaces
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Subscribe<TEvent, THandler>(string subscriberName)
            where TEvent : Event
            where THandler : IEventHandler;
    }
}
