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
    public class ExamRepository: CrudMethods<Exam>
    {
        readonly PeopleDbContext _context = new PeopleDbContext();

        public override bool Create(Exam exam)
        {
            var conString = _context.GetConnectionString();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "INSERT INTO Exams (Grade)" +
                               "VALUES(@EGrade);";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@EGrade", SqlDbType.Float).Value = exam.Grade;            
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

        public override void Update(int id, string grade)
        {
            float fgrade = 0;
            if (grade.Contains("."))
            {
                grade = grade.Replace(".", ",");
                fgrade = float.Parse(grade);  //converte  value to float from string               
            }

            var conString = _context.GetConnectionString();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "UPDATE Exams SET Grade = @EGrade WHERE StudentId = @Sid;";
                SqlCommand cmd = new SqlCommand(query, con);              
                cmd.Parameters.AddWithValue("@Sid",id);
                cmd.Parameters.Add("@EGrade", SqlDbType.Float).Value = Math.Round(fgrade, 2);
                cmd.ExecuteNonQuery();
            }
        }      
    }
}