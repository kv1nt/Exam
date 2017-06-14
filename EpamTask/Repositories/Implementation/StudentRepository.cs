using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EpamTask.Models;
using EpamTask.Repositories.BaseCrudMethods;

namespace EpamTask.Repositories.Implementation
{
    public class StudentRepository: CrudMethods<People>
    {     
        private readonly PeopleDbContext _context = new PeopleDbContext();
      
        public override  IEnumerable<People> GetAll()
        {
      
            //read string from app.config file with key "ConnectionString"
            var conString = _context.setConnectionString();
            //create list for save data from db and return as result of function
            List<People> people = new List<People>();

            //create and use connection to SQL db
            using (var connection = new SqlConnection(conString))
            {
                //create command text whith we will use in SQLCommand
                string commandText = @"SELECT Id, FirstName, LastName
                                        FROM People";

                //create SqlCommand with early entered text and connection
                var command = new SqlCommand(commandText, connection);              
               
                //open sql connection
                connection.Open();
                //execute and using reader for get results of command executing
                using (var reader = command.ExecuteReader())
                {
                    //read data from reader
                    while (reader.Read())
                    {
                        //create new People from db data
                        var item = new People
                        {
                            Id = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                          };
                        //add created people to result list
                        people.Add(item);
                    }
                }
            }
            //return result list
            return people;
        }

        public override int FindIdEntity(int id)
        {
            var conString = _context.setConnectionString();
            using (var connection = new SqlConnection(conString))
            {
                string commandText = @"SELECT Id FROM People WHERE Id = @PId;";
                var command = new SqlCommand(commandText, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    }
                }
            }
            return id;
        }

        public override People Get(int id)
        {     
            People people = new People();
            var conString = _context.setConnectionString();
            using (var connection = new SqlConnection(conString))
            {
                string commandText = @"SELECT Id, FirstName, LastName,
                                     HireDate, ExamDate, Description FROM People WHERE Id =" + id;
                var command = new SqlCommand(commandText, connection);           
            connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         people = new People
                        {
                            Id = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            HireDate = Convert.ToDateTime(reader[3].ToString()),
                            ExamDate = Convert.ToDateTime(reader[4].ToString()),
                            Description = reader[5].ToString()
                         };
                     }
                }
            }
            //return result list
            return people;
        }

        public override bool Create(People people)
        {
            var conString = _context.setConnectionString();

            using (SqlConnection con = new SqlConnection(conString))
            {

                string query = "INSERT INTO People (FirstName, LastName, HireDate, ExamDate, Description)" +
                               "VALUES(@PFirstName, @PLastName , @PHireDate, @PExamDate, @PDescription);";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar, 50).Value = people.FirstName;
                cmd.Parameters.Add("@PLastName", SqlDbType.VarChar, 50).Value = people.LastName;
                cmd.Parameters.Add("@PHireDate", SqlDbType.DateTime, 50).Value = people.HireDate;
                cmd.Parameters.Add("@PExamDate", SqlDbType.DateTime, 50).Value = people.ExamDate;
                cmd.Parameters.Add("@PDescription", SqlDbType.VarChar, 250).Value = people.Description;

                con.Open();
                var i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}