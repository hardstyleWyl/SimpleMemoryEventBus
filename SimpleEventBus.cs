using System.Reflection;

namespace SimpleMemoryEventBus
{
    /// <summary>
    /// 超简单的事件总线
    /// </summary>
    public class SimpleEventBus : ISimpleEventBus
    {
        private readonly Dictionary<long, List<object>> _dicEventHandler = new Dictionary<long, List<object>>();
        private readonly object lockObj = new object();
        public SimpleEventBus(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null || assemblies.Count() == 0)
            {
                throw new Exception("assemblies not null or empty");
            }
            Initializer(assemblies);
        }

        /// <summary>
        /// 初始化EventBus
        /// </summary>
        /// <param name="assembly">handle所在程序集</param>
        /// <returns></returns>
        private void Initializer(IEnumerable<Assembly> assembly)
        {
            foreach (var asm in assembly)
            {
                var bindHandles = asm
                 .GetTypes().Where(t =>
                 !t.IsAbstract &&
                 t.IsDefined(typeof(BindSimpleEventHandlerAttribute)));

                foreach (var type in bindHandles)
                {
                    var attr = type.GetCustomAttribute<BindSimpleEventHandlerAttribute>();
                    var handle = Activator.CreateInstance(type) as ISimpleEventHandler;
                    if (handle == null)
                    {
                        throw new InvalidCastException("转换失败");
                    }
                    Subscribe(attr!.MethodId, handle);
                }
            }
        }

        public void Subscribe(long eventId, ISimpleEventHandler eventHandler)
        {
            lock (lockObj)
            {
                //若存在key则添加处理
                if (_dicEventHandler.ContainsKey(eventId))
                {
                    var handles = _dicEventHandler[eventId];
                    //key存在观察值是否为空
                    if (handles != null)
                    {
                        _dicEventHandler[eventId].Add(eventHandler);
                    }
                    else
                    {
                        handles = new List<object>() { eventHandler };
                    }
                }
                else
                {
                    _dicEventHandler.Add(eventId, new List<object>() { eventHandler });
                }

            }
        }

        public void Unsubscribe(long eventId)
        {
            if (_dicEventHandler.ContainsKey(eventId))
            {
                _dicEventHandler.Remove(eventId);
            }
        }

        public async Task Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception?>? callback)
            where TEvent : SimpleEvent
        {
            try
            {
                await Publish(@event);
                callback?.Invoke(@event, true, null);
            }
            catch (Exception ex)
            {
                callback?.Invoke(@event, false, ex);
            }

        }

        public async Task Publish<TEvent>(TEvent @event)
            where TEvent : SimpleEvent
        {
            if (_dicEventHandler.ContainsKey(@event.EventId) &&
                _dicEventHandler[@event.EventId] != null &&
                _dicEventHandler[@event.EventId].Count > 0)
            {
                var handlers = _dicEventHandler[@event.EventId];
                foreach (var handler in handlers)
                {
                    var eventHandle = handler as ISimpleEventHandler<TEvent>;
                    if (eventHandle != null)
                        await eventHandle.Handle(@event);
                }

            }

        }


    }
}


