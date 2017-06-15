using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EpamTask.Models
{
    public class PeopleDbContext
    {
        public string GetConnectionString()
        {
            string connectionString = ConfigurationManager.AppSettings["dbConnection"];
            return connectionString;
        }
        
    }
}
