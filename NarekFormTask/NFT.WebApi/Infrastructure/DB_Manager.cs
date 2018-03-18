using NFT.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
    }
}