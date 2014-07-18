using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropDBOperations
{

    class UserNotificationOptions
    {
        private string mail;
        private TimeSpan iFrom;
        private TimeSpan iTo;
        private Boolean mailToggle;
        private Boolean desktopToggle;
        private int interval;

        public UserNotificationOptions(string Mail, TimeSpan IFrom, TimeSpan ITo, Boolean MailToggle, Boolean DesktopToggle, int Interval)
        {
            this.Mail = Mail;
            this.IFrom = IFrom;
            this.ITo = ITo;
            this.MailToggle = MailToggle;
            this.DesktopToggle = DesktopToggle;
            this.Interval = Interval;
        }

        public string Mail
        {
            get;
            set;
        }
        public TimeSpan IFrom
        {
            get;
            set;
        }
        public TimeSpan ITo
        {
            get;
            set;
        }
        public Boolean MailToggle
        {
            get;
            set;
        }
        public Boolean DesktopToggle
        {
            get;
            set;
        }
        public int Interval
        {
            get;
            set;
        }

        public String ToString()
        {
            return this.Mail + " " + this.IFrom + " " + this.ITo + " " + this.MailToggle + " " + this.DesktopToggle + " " + this.Interval;
        }


    }

}
