using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamTask.Models
{
    public class AllModels 
    {
        public People People { get; set; }
        public Exam Exam { get; set; }
        public Subject Subject { get; set; }
        public ProfessorSubject ProfessorSubject { get; set; }
        
    }
}