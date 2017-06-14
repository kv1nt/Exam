using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamTask.Models
{
    public class ProfessorSubject: IdEntity
    {
        public int SubjectId { get; set; }
        public string ProfessorInfo { get; set; }     
    }
}