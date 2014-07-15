using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInClasses
{
    class User
    {
        private string Mail;
        private string Pass;

        public User(string Mail, string Pass)
        {
            this.Mail = Mail;
            this.Pass = Pass;
        }

        public string ToString()
        {
            return this.Mail + " " + this.Pass;
        }
    }
}
