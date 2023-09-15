using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

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

        public void Update(int id, Employee? emp)
        {
            using(IDbConnection db = new SqlConnection(conn))
            { 
                StringBuilder sql = new StringBuilder("UPDATE Employee SET ");
                if(emp != null)
                {
                    var param = new DynamicParameters();
                    param.Add("EmployeeId", id);
                    if (emp.Name != null)
                    {
                        sql.Append($"name = @Name");
                        param.Add("Name",emp.Name);
                    }
                    if (emp.Passport != null)
                    {
                        sql.Append($", surname = @Surname");
                        param.Add("Surname", emp.Surname);
                    }
                    if (emp.Phone != null)
                    {
                        sql.Append($", phone = @Phone");
                        param.Add("Phone", emp.Phone);
                    }
                    if (emp.CompanyId > -1)
                    {
                        sql.Append($", companyId = @CompanyId");
                        param.Add("CompanyId", emp.CompanyId);
                    }
                    if (emp.Passport != null && emp.Passport.Number!=null)
                    {
                        sql.Append($", passportId = (SELECT passportId FROM Passport WHERE number = @PassportNumber)");
                        param.Add("PassportNumber", emp.Passport.Number);
                    }
                    if (emp.Department != null && emp.Department.Name!=null)
                    {
                        sql.Append($", departmentId = (SELECT departmentId FROM Department WHERE name = @DepartmentName)");
                        param.Add("DepartmentName", emp.Department.Name);
                    }
                    sql.Append($" WHERE employeeId = @EmployeeId");
                    db.Execute(sql.ToString(), param);
                }
            }
        }
    }
}
