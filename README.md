#  SimpleMemoryEventBus

## 1.��д�¼����¼�������

```c#
//�¼�
//�̳�SimpleEvent����չ�¼���Ҫ���ݵ�����
public class SendPublicMessageEvent : SimpleEvent
{
        public string Message { get; set; }
}
//�¼�������
//BindSimpleHandle���Ա�ע������һ�������������ں�����ע���¼�����������������Ϊ�¼�id
//ISimpleEventHandler<T> T:��ʾҪ������¼�
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

## 2.ע��EventBus��IOC�У�������.NetCore�Դ�IOC��

```c#
//��ȡISimpleEventHandlerʵ�������ڵĳ���
var asm = typeof(T).Assembly;
//ע��ʱЯ�����򼯲�������
services.AddSimpleMemoryEventBus(new[] { asm });
```

## 3.����һ���¼�

```c#
var @event = new SendPublicMessageEvent() { EventId = 101, Message = "666" };
eventBus.Publish(@event);
//֮�����Ǿͻῴ������̨��ӡ��SendPublicMessageHandle:666��˵���ɹ��ˣ�
```

