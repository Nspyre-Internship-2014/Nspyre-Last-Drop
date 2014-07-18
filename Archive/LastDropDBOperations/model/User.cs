using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropDBOperations
{
    class User
    {
        private string mail;
        private string pass;

        public User(string Mail, string Pass)
        {
            this.Mail = Mail;
            this.Pass = Pass;
        }

        public string ToString()
        {
            return this.Mail + " " + this.Pass;
        }

        public string Mail{ get; set; }

        public string Pass{ get; set; }
    }
}
