using System.Text.Json.Serialization;

namespace InventarioAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        EmEstoque,
        Esgotado,
    }
}
