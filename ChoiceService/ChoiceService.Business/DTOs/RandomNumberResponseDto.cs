using System.Text.Json.Serialization;

namespace ChoiceService.Business.DTOs
{
    public class RandomNumberResponseDto
    {
        [JsonPropertyName("random_number")]
        public int RandomNumber { get; set; }
    }
}
