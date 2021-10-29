using System;
using DevNullCore.Bus.Interfaces;

namespace DevNullCore.Bus
{
    public class Event : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime OccurredOn { get; set; }

        public Event()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
        }
    }
}