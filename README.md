#  SimpleMemoryEventBus

## 1.编写事件及事件处理器

```c#
//事件
//继承SimpleEvent以扩展事件需要传递的数据
public class SendPublicMessageEvent : SimpleEvent
{
        public string Message { get; set; }
}
//事件处理器
//BindSimpleHandle特性标注了这是一个处理器，便于后续的注册事件处理器操作，参数为事件id
//ISimpleEventHandler<T> T:表示要处理的事件
[BindSimpleHandle(101)]
public class SendPublicMessageHandle : ISimpleEventHandler<SendPublicMessageEvent>
{
        public Task Handle(SendPublicMessageEvent @event)
        {
            Console.WriteLine("SendPublicMessageHandle:" + @event.Message);
            return Task.CompletedTask;
        }
}
```

## 2.注册EventBus到IOC中（这里是.NetCore自带IOC）

```c#
//获取ISimpleEventHandler实现类所在的程序集
var asm = typeof(T).Assembly;
//注入时携带程序集参数即可
services.AddSimpleMemoryEventBus(new[] { asm });
```

## 3.发布一个事件

```c#
var @event = new SendPublicMessageEvent() { EventId = 101, Message = "666" };
eventBus.Publish(@event);
//之后我们就会看到控制台打印出SendPublicMessageHandle:666就说明成功了！
```

