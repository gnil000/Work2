using Newtonsoft.Json;

namespace Work2.Models
{
    public class CreateEmployeeModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public string DepartmentName { get; set; }
        [JsonProperty("passport")]
        public Passport Passport { get; set; }
    }
}
