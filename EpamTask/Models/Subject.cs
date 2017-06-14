using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamTask.Models
{
    public class Subject: IdEntity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
  
    }
}