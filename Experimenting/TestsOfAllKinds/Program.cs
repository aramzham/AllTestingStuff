using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestsOfAllKinds.Models;

namespace TestsOfAllKinds
{
    // Math.Floor() function:   The two numbers 123.456 and 123.987 are rounded down to the nearest integer.This means that regardless of how close they are close to 124, they are rounded to 123.
    //Note:
    //Floor can be useful when rounding numbers that are part of a larger representation of another number.

    //Discussion.The Math.Floor method when given a positive number will erase the digits after the decimal place. But when it receives a negative number, it will erase the digits and increase the number's negativity by 1.
    //So:
    //Using Math.Floor on a negative number will still decrease the total number.This means it will always become smaller.
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder();
                builder.DataSource = @"Computer\SqlExpress";
                builder.InitialCatalog = "SuccinctlyExamples";
                builder.IntegratedSecurity = true;

                var people = new List<Person>();
                var genders = new List<Gender>();
                
                using (var connection = new SqlConnection(builder.ConnectionString))
                using(var command = new SqlCommand("Select Id, FirstName, LastName, DateOfBirth, GenderId from Person;" + "Select Id, Code, Description from Gender;", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var p = new Person();

                            var idByIndex = reader[0];
                            var idByIndexCast = Convert.ToInt32(idByIndex);

                            var idByName = reader[nameof(Person.Id)];
                            var idByNameCast = Convert.ToInt32(idByName);

                            // if name of property and name of column in db don't match, the code will break
                            var idIndex = reader.GetOrdinal(nameof(Person.Id));
                            p.Id = reader.GetInt32(idIndex);

                            var firstNameIndex = reader.GetOrdinal(nameof(Person.FirstName));
                            p.FirstName = reader.GetString(firstNameIndex);

                            var lastNameIndex = reader.GetOrdinal(nameof(Person.LastName));
                            if (!reader.IsDBNull(lastNameIndex))
                                p.LastName = reader.GetString(lastNameIndex);

                            var dateOfBirthIndex = reader.GetOrdinal(nameof(Person.DateOfBirth));
                            if (!reader.IsDBNull(dateOfBirthIndex))
                                p.DateOfBirth = reader.GetDateTime(dateOfBirthIndex);

                            var genderIndex = reader.GetOrdinal(nameof(Person.GenderId));
                            if (!reader.IsDBNull(genderIndex))
                                p.GenderId = reader.GetInt32(genderIndex);

                            people.Add(p);
                        }

                        // for many results
                        reader.NextResult();

                        while (reader.Read())
                        {
                            var g = new Gender();
                            g.Id = reader.GetInt32(0);
                            g.Code = reader.GetString(1);
                            g.Description = reader.GetString(2);

                            genders.Add(g);
                        }
                    }
                }

                Console.WriteLine("Successfully opened and closed the database");

                foreach (var person in people)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName} was born on {person.DateOfBirth}");
                }

                foreach (var gender in genders)
                {
                    Console.WriteLine($"{gender.Id} - {gender.Code}, {gender.Description}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
