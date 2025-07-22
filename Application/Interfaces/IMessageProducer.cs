namespace Application.Interfaces
{
    public interface IMessageProducer
    {
        Task SendMessage<T>(T message);
    }
}
