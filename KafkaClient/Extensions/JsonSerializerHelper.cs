using System.Text.Json;
using System.Text.Json.Serialization;

namespace KafkaClient.Extensions;

public static class JsonSerializerHelper
{
    private static readonly JsonSerializerOptions? JsonSerializerOptions = new()
    {
         PropertyNameCaseInsensitive =  true,
         DefaultIgnoreCondition =  JsonIgnoreCondition.WhenWritingNull,
         Converters = { new JsonStringEnumConverter() }
    };

    public static TOutput Deserialize<TOutput>(string json)
    {
        return JsonSerializer.Deserialize<TOutput>(json, JsonSerializerOptions)!;
    }
}