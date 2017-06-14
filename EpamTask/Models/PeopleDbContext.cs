using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamTask.Models
{
    public class PeopleDbContext
    {
        public string setConnectionString()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentDbModels;" +
                          "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" +
                          "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return connectionString;
        }
        
    }
}
