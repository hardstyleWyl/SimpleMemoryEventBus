namespace SimpleMemoryEventBus
{
    public interface ISimpleEventBus
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventId">订阅事件id</param>
        /// <param name="eventHandler">要订阅的事件</param>
        void Subscribe(long eventId, ISimpleEventHandler eventHandler);
        /// <summary>
        /// 解除订阅
        /// </summary>
        /// <param name="eventId"></param>
        void Unsubscribe(long eventId);
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event">事件源信息</param>
        /// <param name="callback">回调函数，可操作handle之后的值，及根据标志是否出现异常获取异常</param>
        Task Publish<TEvent>(TEvent @event) where TEvent : SimpleEvent;
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event">事件资源</param>
        /// <returns></returns>
        Task Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception?> callback) where TEvent : SimpleEvent;
    }
}
