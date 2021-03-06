using Ereceipt.Application.Services.Interfaces;
using System.Text.Json;

namespace Ereceipt.Application.Services.Implementations
{
    public class JsonConverter : IJsonConverter
    {
        public T GetModelFromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public string GetStringAsJson<T>(T model)
        {
            return JsonSerializer.Serialize(model);
        }

        public string GetStringAsJson(object model)
        {
            return JsonSerializer.Serialize(model);
        }
    }
}