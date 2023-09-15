using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Work2.Models;

namespace Work2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        IDepartmentRepository dr;
        public DepartmentController(IDepartmentRepository dr)
        {
            this.dr = dr;
        }

        [HttpGet]
        [Route("{id}")]
        public Department GetDepartmentById(int id)
        {
            return dr.GetDepartment(id);
        }

        [HttpGet]
        public List<Department> GetAll()
        {
            return dr.GetAll();
        }

        [HttpPost]
        public int AddDepartment(Department dep)
        {
            return dr.Create(dep);
        }

    }
}
