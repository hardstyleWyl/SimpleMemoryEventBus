using System.Text.Json;

namespace SimpleMemoryEventBus.Extensions
{
    public static class JsonToEventStringExtension
    {
        public static TEvent? ToEnvent<TEvent>(this string json) where TEvent : SimpleEvent
        {
            return JsonSerializer.Deserialize<TEvent>(json);
        }
    }
}
