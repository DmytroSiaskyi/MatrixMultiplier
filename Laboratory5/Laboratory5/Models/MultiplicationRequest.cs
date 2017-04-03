using Newtonsoft.Json;

namespace Laboratory5.Models
{
    public class MultiplicationRequest
    {
        [JsonProperty("first_array")]
        public double[,] FirstArray { get; set; }

        [JsonProperty("second_array")]
        public double[,] SecondArray { get; set; }
    }
}