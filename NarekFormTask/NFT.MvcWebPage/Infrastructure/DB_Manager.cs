using NFT.MvcWebPage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NFT.MvcWebPage.Infrastructure
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

        public void AddEmployee(Employee employee)
        {
            var query = "INSERT INTO Employee" +
                           "VALUES (@Name, @Surname, @Salary, @IsGettingBonus, @UniversityId, @Info)";

            // create connection and command
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = employee.Name;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 150).Value = employee.Surname;
                cmd.Parameters.Add("@Salary", SqlDbType.Money).Value = employee.Salary;
                cmd.Parameters.Add("@IsGettingBonus", SqlDbType.Bit).Value = employee.IsGettingBonus;
                cmd.Parameters.Add("@UniversityId", SqlDbType.Int).Value = employee.UniversityId;
                cmd.Parameters.Add("@Info", SqlDbType.VarChar, 500).Value = employee.Info;

                // open connection, execute INSERT, close connection
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}