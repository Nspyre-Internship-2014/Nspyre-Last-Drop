using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropMainServer
{
    [Serializable]
    public class User
    {
        private string mail;
        private string pass;

        private User()
        { }

        public User(string Mail, string Pass)
        {
            this.mail = Mail;
            this.pass = Pass;
        }

        public string ToString()
        {
            return this.mail + " " + this.pass;
        }

        public bool Equals(User u)
        {
            // If parameter is null return false:
            if ((object)u == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.mail == u.Mail) && (this.pass == u.Pass);
        }

        public string Mail
        {
            get { return this.mail; }
            set { this.mail = value; }
        }

        public string Pass
        {
            get { return this.pass; }
            set { this.pass = value; }
        }
    }
}