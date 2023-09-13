using System.Text.Json.Serialization;

namespace Work2.Models
{
    public class Department
    {
        [JsonIgnore]
        public int departmentId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
