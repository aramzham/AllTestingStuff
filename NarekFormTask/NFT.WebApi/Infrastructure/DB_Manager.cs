using NFT.WebApi.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NFT.WebApi.Infrastructure
{
    public class DB_Manager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["EmployeeDb"].ConnectionString;

        public async Task<List<Employee>> GetAllEmployees()
        {
            const string query = "select * from employee";
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();

                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    employees.Add(new Employee()
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Surname = (string)reader[2],
                        IsGettingBonus = (bool)reader["IsGettingBonus"],
                        Salary = (decimal)reader["Salary"],
                        UniversityId = (int)reader["UniversityId"],
                        Info = reader["Info"] as string
                    });
                }
                reader.Close();
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            const string query = "select * from employee where id = @Id";
            Employee emp = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id).Direction = ParameterDirection.Input;
                //outputParam.Direction = ParameterDirection.Input;

                conn.Open();

                using (IDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        emp = new Employee()
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Surname = (string)reader[2],
                            IsGettingBonus = (bool)reader["IsGettingBonus"],
                            Salary = (decimal)reader["Salary"],
                            UniversityId = (int)reader["UniversityId"],
                            Info = reader["Info"] as string
                        };
                    }
                }
                return emp;
            }
        }
    }
}