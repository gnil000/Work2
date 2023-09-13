using Microsoft.AspNetCore.Mvc;
using Work2.Models;

namespace Work2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        IEmployeeRepository rp;
        public EmployeeController(IEmployeeRepository rp)
        {
            this.rp = rp;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return rp.GetAll();
        }

        [HttpGet]
        [Route("GetByDep")]
        public List<Employee> GetAllByDepartment(string depName)
        {
            return rp.GetAllByDepartmentName(depName);
        }

        [HttpGet]
        [Route("GetByCom")]
        public List<Employee> GetAllByCompany(int id)
        {
            return rp.GetAllByCompanyId(id);
        }

        [HttpGet]
        [Route("{id}")]
        public Employee? Get(int id)
        {
            return rp.GetEmployee(id);
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            int id = rp.Create(emp);
            return Ok(new { ID = id });
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(int id, Employee emp)
        {
            var employee = rp.GetEmployee(id);
            if(employee==null)
                NotFound();
            rp.Update(emp);
            return Ok(emp);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            rp.Delete(id);
            return Ok();
        }

    }
}
