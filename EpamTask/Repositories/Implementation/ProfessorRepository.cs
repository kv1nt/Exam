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
    
    public class ProfessorRepository: CrudMethods<ProfessorSubject>
    {
        private readonly PeopleDbContext _context = new PeopleDbContext();
        public override IEnumerable<ProfessorSubject> GetAll()
        {

            //read string from app.config file with key "ConnectionString"
            var conString = _context.GetConnectionString();
            //create list for save data from db and return as result of function
            List<ProfessorSubject> professorsList = new List<ProfessorSubject>();

            //create and use connection to SQL db
            using (var connection = new SqlConnection(conString))
            {
                //create command text whith we will use in SQLCommand
                string commandText = @"SELECT Id, SubjectId, ProfessorInfo
                                                FROM SubjectProfessor;";

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
                        var item = new ProfessorSubject
                        {
                            Id = int.Parse(reader[0].ToString()),
                            SubjectId = int.Parse(reader[1].ToString()),
                            ProfessorInfo = reader[2].ToString()                      
                        };
                       
                        //add created people to result list
                        professorsList.Add(item);
                    }
                }
            }
            //return result list
            return professorsList;
        }

        public override bool Create(string professorInfo, int id)
        {
            var conString = _context.GetConnectionString();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "INSERT INTO SubjectProfessor (ProfessorInfo, SubjectId)" +
                               "VALUES(@PprofessorInfo, @SSubjectId);";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@PprofessorInfo", SqlDbType.VarChar, 250).Value = professorInfo;
                cmd.Parameters.Add("@SSubjectId", SqlDbType.Int).Value = id;
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