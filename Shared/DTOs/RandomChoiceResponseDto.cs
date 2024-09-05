using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class RandomChoiceResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
