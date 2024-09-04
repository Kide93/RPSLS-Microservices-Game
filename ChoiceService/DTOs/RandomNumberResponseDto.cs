using System.Text.Json.Serialization;

namespace ChoiceService.DTOs
{
    public class RandomNumberResponseDto
    {
        [JsonPropertyName("random_number")]
        public int RandomNumber { get; set; }
    }
}
