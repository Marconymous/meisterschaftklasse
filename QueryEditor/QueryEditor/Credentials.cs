using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryEditor
{

   
    public class Credentials
    {
        public string Server { get; }
        public string User { get; }
        public string Password { get; }
        public string Database { get; }

        public Credentials(string server, string user, string password, string database)
        {
            Server = server;
            User = user;
            Password = password;
            Database = database;
        }
    }
}
