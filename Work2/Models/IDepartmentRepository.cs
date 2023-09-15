namespace Work2.Models
{
    public interface IDepartmentRepository
    {
        Department GetDepartment(int id);
        int Create(Department dep);
        List<Department> GetAll();
    }
}
