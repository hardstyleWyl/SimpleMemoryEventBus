namespace SimpleMemoryEventBus
{
    /// <summary>
    /// 绑定事件处理器到EventBus上
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BindSimpleEventHandlerAttribute : Attribute
    {
        public long MethodId { get; set; }

        public BindSimpleEventHandlerAttribute(long methodId)
        {
            MethodId = methodId;
        }
    }
}
