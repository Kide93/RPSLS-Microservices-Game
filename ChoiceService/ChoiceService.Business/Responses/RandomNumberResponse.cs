using System.Text.Json.Serialization;

namespace ChoiceService.Business.Responses
{
    public class RandomNumberResponse
    {
        [JsonPropertyName("random_number")]
        public int RandomNumber { get; set; }
    }
}
