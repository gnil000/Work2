using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Work2.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        string conn = null;
        public EmployeeRepository(string conn)
        {
            this.conn = conn;
        }

        public int Create(Employee emp)
        {
            string passportSql = @"INSERT INTO Passport(type, number)
            VALUES (@Type, @Number);
            SELECT SCOPE_IDENTITY();";

            string depSql = @$"
               select departmentId from Department where name = @name;
            ";

            string empSql = @"INSERT INTO Employee (Name, Surname, Phone, CompanyId, passportId, departmentId)
            VALUES (@Name, @Surname, @Phone, @CompanyId, @passportId, @departmentId);
            SELECT SCOPE_IDENTITY();";

            using (IDbConnection db = new SqlConnection(conn))
            {
                int passId = db.ExecuteScalar<int>(passportSql, new { emp.Passport.Type, emp.Passport.Number});
                int depId = db.ExecuteScalar<int>(depSql, new { emp.Department.Name});
                emp.Passport.passportId = passId;
                emp.Department.departmentId = depId;
                int empId = db.ExecuteScalar<int>(empSql, new
                {
                    emp.Name,
                    emp.Surname,
                    emp.Phone,
                    emp.CompanyId,
                    emp.Passport.passportId,
                    emp.Department.departmentId
                });
                return empId;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(conn))
            {
                var sql = "DELETE FROM Employee Where employeeId=@id";
                db.Execute(sql, new { id});
            }
        }

        public List<Employee> GetAll()
        {
            using (IDbConnection db = new SqlConnection(conn))
            {
                var sql = @"SELECT  e.employeeId, e.Name, e.Surname, e.Phone, e.CompanyId, p.passportId, p.Type,
        p.Number, d.departmentId, d.Name, d.Phone
		FROM Employee e
		INNER JOIN Passport p ON p.passportId = e.passportId
		INNER JOIN Department d ON d.departmentId = e.departmentId";
                var res = db.Query<Employee, Passport, Department, Employee>(sql, (emp, pass, dep) => {
                    emp.Passport = pass;
                    emp.Department = dep;
                    return emp;                                           
                },splitOn: "passportId, departmentId").ToList();
                return res;
            }
        }

        public Employee? GetEmployee(int id)
        {
            using (IDbConnection db = new SqlConnection(conn))
            {
                var sql = @$"SELECT  e.employeeId, e.Name, e.Surname, e.Phone, e.CompanyId, p.passportId, p.Type,
        p.Number, d.departmentId, d.Name, d.Phone
		FROM Employee e
		INNER JOIN Passport p ON p.passportId = e.passportId
		INNER JOIN Department d ON d.departmentId = e.departmentId
        WHERE e.employeeId = {id}";
                var emp = db.Query<Employee, Passport, Department, Employee>(sql, (emp, pass, dep) => {
                    emp.Passport = pass;
                    emp.Department = dep;
                    return emp;
                }, splitOn: "passportId, departmentId").FirstOrDefault();
                return emp;
            }
        }

        public List<Employee> GetAllByDepartmentName(string departmentName)
        {
            using (IDbConnection db = new SqlConnection(conn))
            {
                var sql = @$"SELECT  e.employeeId, e.Name, e.Surname, e.Phone, e.CompanyId, p.passportId, p.Type,
        p.Number, d.departmentId, d.Name, d.Phone
		FROM Employee e
		INNER JOIN Passport p ON p.passportId = e.passportId
		INNER JOIN Department d ON d.departmentId = e.departmentId
		WHERE d.NAME = '{departmentName}'";
                var emp = db.Query<Employee, Passport, Department, Employee>(sql, (emp, pass, dep) => {
                    emp.Passport = pass;
                    emp.Department = dep;
                    return emp;
                }, splitOn: "passportId, departmentId").ToList();
                return emp;
            }
        }

        public List<Employee> GetAllByCompanyId(int id)
        {
            using (IDbConnection db = new SqlConnection(conn))
            {
                var sql = @$"SELECT  e.employeeId, e.Name, e.Surname, e.Phone, e.CompanyId, p.passportId, p.Type,
        p.Number, d.departmentId, d.Name, d.Phone
		FROM Employee e
		INNER JOIN Passport p ON p.passportId = e.passportId
		INNER JOIN Department d ON d.departmentId = e.departmentId
		WHERE e.companyId = {id}";
                var emp = db.Query<Employee, Passport, Department, Employee>(sql, (emp, pass, dep) => {
                    emp.Passport = pass;
                    emp.Department = dep;
                    return emp;
                }, splitOn: "passportId, departmentId").ToList();
                return emp;
            }
        }

        public void Update(Employee emp)
        {
            using(IDbConnection db = new SqlConnection(conn))
            {
                var upF = new List<string>();
                var param = new DynamicParameters();
                param.Add("employeeId", emp.employeeId);

                if (!string.IsNullOrEmpty(emp.Name))
                {
                    upF.Add("Name = @Name");
                    param.Add("Name",emp.Name);
                }
                if (!string.IsNullOrEmpty(emp.Surname))
                {
                    upF.Add("Surname = @Surname");
                    param.Add("Surname", emp.Surname);
                }
                if (!string.IsNullOrEmpty(emp.Phone))
                {
                    upF.Add("Phone = @Phone");
                    param.Add("Phone", emp.Phone);
                }

                if (emp.CompanyId > -1)
                {
                    upF.Add("CompanyId = @CompanyId");
                    param.Add("CompanyId", emp.CompanyId);
                }

                if (upF.Any())
                {
                    var sql = $"UPDATE Employee SET {string.Join(", ", upF)} WHERE employeeId = @employeeId";
                    db.Execute(sql, param);
                }

            }
        }
    }
}
