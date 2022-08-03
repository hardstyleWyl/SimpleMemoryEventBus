namespace SimpleMemoryEventBus
{
    /// <summary>
    /// 简单事件，如需扩展事件传递的内容则需继承进行扩展
    /// </summary>
    public class SimpleEvent
    {
        public long EventId { get; set; }
    }
}
