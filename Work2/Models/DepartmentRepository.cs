using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Work2.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {

        string conn = null;
        public DepartmentRepository(string conn)
        {
            this.conn = conn;
        }

        public int Create(Department dep)
        {
            string departmentSql = @"INSERT INTO Department(name, phone)
            VALUES (@Name, @Phone);
            SELECT SCOPE_IDENTITY();";

            using (IDbConnection db = new SqlConnection(conn))
            {
                int depId = db.ExecuteScalar<int>(departmentSql, new { dep.Name, dep.Phone});
                return depId;
            }
        }

        public Department? GetDepartment(int id)
        {
            var sql = "SELECT * FROM Department WHERE departmentId=@id";

            using (IDbConnection db = new SqlConnection(conn))
            {
                return db.Query<Department>(sql, new { id}).FirstOrDefault();
            }
        }

        public List<Department> GetAll()
        {
            var sql = "SELECT * FROM Department";

            using (IDbConnection db = new SqlConnection(conn))
            {
                return db.Query<Department>(sql).ToList();
            }
        }

    }
}
