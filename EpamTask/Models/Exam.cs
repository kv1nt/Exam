using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamTask.Models
{
    public class Exam: IdEntity
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public double Grade { get; set; }
    }
}