using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LastDropMainServer.validator
{
    class Validator
    {

       public Validator()
        {
        }

        public bool validateMail(String mail)
        {
            Regex r = new Regex("^[a-zA-Z0-9@._]*$");
            if (r.IsMatch(mail))
                return true;
            else
                return false;
        }

        public bool validatePassword(String password)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (r.IsMatch(password))
                return true;
            else
                return false;
        }

        public bool validateRegister(String mail, String password)
        {
            if ((validateMail(mail) == true) && (validatePassword(password) == true))
                return true;
            else
                return false;
        }
    }
}
