using System.Text.Json.Serialization;

namespace InventarioAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoriaEnum
    {
        Eletronicos,
        Vestuario,
        Livros,
        Esportes,
        Brinquedos,
    }
}
