using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using EpamTask.Models;
using EpamTask.Repositories.BaseCrudMethods;

namespace EpamTask.Repositories.Implementation
{
    public class AlIExamInfoRepository: CrudMethods<AllModels>
    {
        readonly PeopleDbContext _context = new PeopleDbContext();
      
        public override AllModels Get(int id)
        {
            AllModels allModels = new AllModels();
            var conString = _context.GetConnectionString();
            using (var connection = new SqlConnection(conString))
            {
                string commandText = @"SELECT exams.id AS examsId, studentId,
                subjectId, FirstName, LastName, HireDate, ExamDate, Grade,
                Title, ShortDescription, Description FROM exams
                INNER JOIN people ON  people.id = exams.studentId AND exams.studentId = @id
                INNER JOIN subjects ON subjects.id = exams.subjectId;";

                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@id", SqlDbType.Int, 10).Value = id;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       People people = new People();
                       Exam exam = new Exam();
                       Subject subject = new Subject();

                        people.Id = reader.GetInt32(0);
                        exam.StudentId = reader.GetInt32(1);
                        exam.SubjectId = reader.GetInt32(2);
                        people.FirstName = reader.GetString(3);
                        people.LastName = reader.GetString(4);
                        people.HireDate = reader.GetDateTime(5);
                        people.ExamDate = reader.GetDateTime(6);
                        exam.Grade = reader.GetDouble(7);
                        subject.Title = reader.GetString(8);
                        subject.ShortDescription = reader.GetString(9);
                        people.Description = reader.GetString(10);

                        allModels.People = people;
                        allModels.Exam = exam;
                        allModels.Subject = subject;
                    }
                }
            }
            //return result allModels
            return allModels;
        }

        private int CreateStudent(AllModels allModels)
        {            
            int studentId;
            string conString =  _context.GetConnectionString();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                var query = "INSERT INTO People (FirstName, LastName, HireDate, ExamDate, Description)" +
                            "VALUES(@PFirstName, @PLastName , @PHireDate, @PExamDate, @PDescription)" +
                            "SELECT SCOPE_IDENTITY()";

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar, 50).Value = allModels.People.FirstName;
                cmd.Parameters.Add("@PLastName", SqlDbType.VarChar, 50).Value = allModels.People.LastName;
                cmd.Parameters.Add("@PHireDate", SqlDbType.DateTime, 50).Value = allModels.People.HireDate;
                cmd.Parameters.Add("@PExamDate", SqlDbType.DateTime, 50).Value = allModels.People.ExamDate;
                cmd.Parameters.Add("@PDescription", SqlDbType.VarChar, 250).Value = allModels.People.Description;
                studentId = Int32.Parse(cmd.ExecuteScalar().ToString());
                cmd.ExecuteNonQuery();
            }
            return studentId;
        }

        public void CreateExam(AllModels allModels)
        {
            var conString = _context.GetConnectionString();
            var subjectId = CreateSubject(allModels);
            var studentId = CreateStudent(allModels);
            using (SqlConnection con2 = new SqlConnection(conString))
            {
                con2.Open();
                var query = "INSERT INTO Exams (StudentId, SubjectId, Grade) " +
                                  "VALUES (@EStudentId, @ESubjectId, @EGrade)";
                var cmd = new SqlCommand(query, con2);
                cmd.Parameters.Add("@EStudentId", SqlDbType.Int).Value = studentId;
                cmd.Parameters.Add("@ESubjectId", SqlDbType.Int).Value = subjectId;
                cmd.Parameters.Add("@EGrade", SqlDbType.Float).Value = allModels.Exam.Grade;
                cmd.ExecuteNonQuery();                
            }
        }

        private int CreateSubject(AllModels allModels)
        {
            var conString = _context.GetConnectionString();
            int subjectId;
            using (SqlConnection con1 = new SqlConnection(conString))
            {
                con1.Open();
                var query = "INSERT INTO Subjects (Title, ShortDescription) " +
                            "VALUES (@STitle, @SShortDesc) SELECT SCOPE_IDENTITY()";
                var cmd = new SqlCommand(query, con1);
                cmd.Parameters.Add("@STitle", SqlDbType.VarChar, 250).Value = allModels.Subject.Title;
                cmd.Parameters.Add("@SShortDesc", SqlDbType.VarChar, 250).Value =
                    allModels.Subject.ShortDescription;
                subjectId = Int32.Parse(cmd.ExecuteScalar().ToString());
                cmd.ExecuteNonQuery();
            }
            return subjectId;
        }

        public override void Delete(int id)
        {
           
            var conString = _context.GetConnectionString();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                var query = "DELETE FROM People  WHERE Id = @Pid;" +
                            "DELETE FROM Exams WHERE StudentId = @Pid;";
                           
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Pid", id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
        }
    }
}