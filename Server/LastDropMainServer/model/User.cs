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

        private User()
        { }

        public User(string Mail, string Pass)
        {
            this.Mail = Mail;
            this.Pass = Pass;
        }

        public string ToString()
        {
            return this.Mail + " " + this.Pass;
        }

        public bool Equals(User u)
        {
            // If parameter is null return false:
            if ((object)u == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Mail == u.Mail) && (this.Pass == u.Pass);
        }

        public string Mail{ get; set; }

        public string Pass{ get; set; }
    }
}
