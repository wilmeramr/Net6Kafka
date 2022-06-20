using Banking.Cqrs.Core.Events;

namespace Banking.Cqrs.Core.Producers
{
    public interface EventProducer
    {
        void Procude(string topic, BaseEvent @event);
    }
}
