using System.Text.Json;

namespace MyApp.SyncAPI.Dtos
{
    public class SyncRequestDto
    {
        public string EntityType { get; set; } = string.Empty;
        public JsonElement Data { get; set; }
    }
}
