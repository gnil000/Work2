namespace Work2.Models
{
    public interface IEmployeeRepository
    {
        int Create(Employee emp);
        void Delete(int id);
        Employee? GetEmployee(int id); //получение работника по id
        List<Employee> GetAll(); // получение всех работников вообще везде
        List<Employee> GetAllByCompanyId(int id); //получение работника по id компании
        List<Employee> GetAllByDepartmentName(string name); //получение работника по названию департамента
        void Update(Employee emp);
    }
}
