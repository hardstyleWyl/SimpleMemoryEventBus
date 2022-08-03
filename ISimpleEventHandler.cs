namespace SimpleMemoryEventBus
{
    public interface ISimpleEventHandler { }
    /// <summary>
    /// 指定其事件的泛型，处理器类实现我便可以处理指定的事件
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface ISimpleEventHandler<TEvent> : ISimpleEventHandler where TEvent : SimpleEvent
    {
        Task Handle(TEvent @event);
    }
}
