using System.Text.Json.Serialization;

namespace Work2.Models
{
    public class Passport
    {
        [JsonIgnore]
        public int passportId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
