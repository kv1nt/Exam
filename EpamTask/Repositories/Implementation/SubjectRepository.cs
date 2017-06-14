using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EpamTask.Models;
using EpamTask.Repositories.BaseCrudMethods;

namespace EpamTask.Repositories.Implementation
{
    public class SubjectRepository: CrudMethods<Subject>
    {
        readonly PeopleDbContext _context = new PeopleDbContext();

        public override Subject Get(int id)
        {
            Subject subjectInfo = new Subject();
            var conString = _context.setConnectionString();
            using (var connection = new SqlConnection(conString))
            {
                string commandText = @"SELECT Id, Title, ShortDescription
                                     FROM Subjects WHERE Id =" + id;
                var command = new SqlCommand(commandText, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjectInfo = new Subject
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Title = reader[1].ToString(),
                            ShortDescription = reader[2].ToString()
                        };
                    }
                }
            }
            return subjectInfo;
        }

        public override IEnumerable<Subject> GetAll()
        {
            var conString = _context.setConnectionString();
            List<Subject> subjects = new List<Subject>();

            using (var connection = new SqlConnection(conString))
            {
                string commandText = @"SELECT Id, Title FROM Subjects";
                var command = new SqlCommand(commandText, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Subject
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Title = reader[1].ToString(),
                        };
                        subjects.Add(item);
                    }
                }
            }
            return subjects;
        }

        public override bool Create(Subject exam)
        {
            var conString = _context.setConnectionString();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "INSERT INTO Subjects (Title, ShortDescription)" +
                               "VALUES(@PTitle, @PShortDesc);";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@PTitle", SqlDbType.VarChar, 250).Value = exam.Title;
                cmd.Parameters.Add("@PShortDesc", SqlDbType.VarChar, 250).Value = exam.ShortDescription;

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