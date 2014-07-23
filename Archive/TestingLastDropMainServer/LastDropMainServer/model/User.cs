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
        #pragma warning disable 0169
        private string mail;
        private string pass;

        private User()
        { }

        public User(string Mail, string Pass)
        {
            this.Mail = Mail;
            this.Pass = Pass;
        }

        public override string ToString()
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
