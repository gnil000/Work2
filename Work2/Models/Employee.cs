using Newtonsoft.Json;

namespace Work2.Models
{
    public class Employee
    {
        public int employeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        [JsonProperty("passport")]
        public Passport Passport { get; set; }
        [JsonProperty("department")]
        public Department Department { get; set; }
    }
}
